using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using Server.Messaging;
using System.Text.Json;
using System.Drawing;

namespace Server
{
    public static class ServerModel
    {
        private const string _swkGUID = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

        private static readonly Dictionary<int, Action<User, string>> _processingMethodsTable =
            new Dictionary<int, Action<User, string>>
        {
            {0, ProcessConnectionMessage},
            {1, ProcessDisonnectionMessage},
            {2, ProcessRegularMessage},
            {3, ProcessDrawMessage},
        };

        private static bool _isDebug = false;

        private static Socket? _sListener;

        private static ConcurrentDictionary<int, User> _users = new ConcurrentDictionary<int, User>();


        public static void Start(string dns, int port)
        {
            try
            {
                IPHostEntry ipHost = Dns.GetHostEntry(dns);
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

                _sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                _sListener.Bind(ipEndPoint);
                _sListener.Listen(10);

                AcceptNewClients(ipEndPoint);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                foreach (var handler in _users.Select(u => u.Value.Socket))
                    CloseClient(handler);
            }
        }

        public static void SendAll(Message? message)
        {
            if (message == null)
                return;

            foreach (var handler in _users.Select(u => u.Value.Socket))
                SendMessage(handler, message);
        }
        
        public static void SendAll(string jsonMessage)
        {
            if (jsonMessage == null)
                return;

            foreach (var handler in _users.Select(u => u.Value.Socket))
                SendMessage(handler, jsonMessage);
        }

        private static void SendMessage(Socket handler, Message? message)
        {
            if (message == null)
                return;

            handler.Send(EncodeReply(message.ToByteArray()));
        }
        
        private static void SendMessage(Socket handler, string jsonMessage)
        {
            if (jsonMessage == null)
                return;

            handler.Send(EncodeReply(Encoding.UTF8.GetBytes(jsonMessage)));
        }

        public static void SetDebugMode(bool isDebug) => _isDebug = isDebug;

        private static void AcceptNewClients(IPEndPoint ipEndPoint)
        {
            Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        ShowDebug($"Waiting for a connection through {ipEndPoint}");

                        Socket handler = _sListener.Accept();

                        ShowDebug("Handler created");

                        //_clientsHandlers.Enqueue(handler);

                        var id = User.GetNewId();
                        var user = new User(handler, id);
                        _users.TryAdd(id, user);

                        Task.Run(() => ProcessClientRequests(user));
                    }
                }
                catch (Exception ex)
                {
                    ShowError(ex);
                    return;
                }
            });
        }

        private static void ProcessClientRequests(User client)
        {
            try
            {
                ShowDebug("Processing started");

                while (true)
                {
                    byte[] requestBin = new byte[10240];
                    int requestByteCount = client.Socket.Receive(requestBin);

                    string request = Encoding.UTF8.GetString(requestBin, 0, requestByteCount);

                    if (Regex.IsMatch(request, "^GET", RegexOptions.IgnoreCase))
                    {
                        HandshakingWithClient(client.Socket, request);
                        continue;
                    }

                    string text = DecodeRequest(requestBin);
                    if (text != string.Empty)
                        Console.WriteLine($"RECIEVED: {text}");

                    ProcessMessage(client, text);

                    if (!IsSocketConnected(client.Socket))
                        throw new Exception("Client has been disconnected");
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
                CloseClient(client.Socket);
                return;
            }
        }

        private static void ProcessMessage(User client, string decodedMessage) =>
            _processingMethodsTable[decodedMessage[15] - 48].Invoke(client, decodedMessage);

        private static void ProcessConnectionMessage(User client, string decodedMessage)
        {
            var reply = (ConnectionMessage?)JsonSerializer.Deserialize(decodedMessage, typeof(ConnectionMessage));
            reply.UserID = client.UserId;

            if (_users.Count == 1)
                reply.IsDrawer = true;

            foreach (var user in _users.Select(u => u.Value))
                SendMessage(user.Socket, reply);
        }

        private static void ProcessDisonnectionMessage(User client, string decodedMessage)
        {
            foreach (var user in _users.Select(u => u.Value))
            {
                if (user.UserId != client.UserId)
                    SendMessage(user.Socket, decodedMessage);
                else
                    _users.TryRemove(user.UserId, out var _);
            }
        }

        private static void ProcessRegularMessage(User client, string decodedMessage) =>
            SendAll(decodedMessage);

        private static void ProcessDrawMessage(User client, string decodedMessage) =>
            SendAll(decodedMessage);

        private static void CloseClient(Socket handler)
        {
            try
            {
                string reply = "--exit";
                byte[] msg = Encoding.UTF8.GetBytes(reply);
                handler.Send(msg);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

                ShowDebug("Handler closed");
            }
            catch (Exception ex)
            {
                ShowError(ex);
                return;
            }
        }

        // 1. Obtain the value of the "Sec-WebSocket-Key" request header without any leading or trailing whitespace
        // 2. Concatenate it with "258EAFA5-E914-47DA-95CA-C5AB0DC85B11" (a special GUID specified by RFC 6455)
        // 3. Compute SHA-1 and Base64 hash of the new value
        // 4. Write the hash back as the value of "Sec-WebSocket-Accept" response header in an HTTP response
        private static void HandshakingWithClient(Socket handler, string clientHandshake)
        {
            ShowDebug($"Handshaking from client\n{clientHandshake}");

            string swk = Regex.Match(clientHandshake, "Sec-WebSocket-Key: (.*)").Groups[1].Value.Trim();
            string swka = swk + _swkGUID;
            byte[] swkaSha1 = System.Security.Cryptography.SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(swka));
            string swkaSha1Base64 = Convert.ToBase64String(swkaSha1);

            // HTTP/1.1 defines the sequence CR LF as the end-of-line marker
            string responseString =
                $"HTTP/1.1 101 Switching Protocols\r\nConnection: Upgrade\r\nUpgrade: websocket\r\nSec-WebSocket-Accept: {swkaSha1Base64}\r\n\r\n";
            byte[] response = Encoding.UTF8.GetBytes(responseString);

            ShowDebug($"Handshaking from server\n{responseString}");

            handler.Send(response);
        }

        public static byte[] EncodeReply(byte[] bytesRaw)
        {
            int newLength = bytesRaw.Length;
            if (bytesRaw.Length <= 125)
                newLength += 2;
            else if (bytesRaw.Length >= 126 && bytesRaw.Length <= 65535)
                newLength += 4;
            else
                newLength += 10;

            byte[] bytesFormatted = new byte[newLength];

            bytesFormatted[0] = 129;

            int offset = 0; // it doesn't matter what value is set here - it will be set now:

            if (bytesRaw.Length <= 125)
            {
                bytesFormatted[1] = (byte)bytesRaw.Length;

                offset = 2;
            }
            else if (bytesRaw.Length >= 126 && bytesRaw.Length <= 65535)
            {
                bytesFormatted[1] = 126;
                bytesFormatted[2] = (byte)((bytesRaw.Length >> 8) & 255);
                bytesFormatted[3] = (byte)((bytesRaw.Length) & 255);

                offset = 4;
            }

            // put raw data at the correct index
            Buffer.BlockCopy(bytesRaw, 0, bytesFormatted, offset, bytesRaw.Length);

            return bytesFormatted;
        }

        public static string DecodeRequest(byte[] requestBin)
        {
            bool mask = (requestBin[1] & 0b10000000) != 0; // must be true, "All messages from the client to the server have this bit set"
            ulong offset = 2;
            ulong msglen = (ulong)(requestBin[1] & 0b01111111);

            if (msglen == 126)
            {
                // bytes are reversed because websocket will print them in Big-Endian, whereas
                // BitConverter will want them arranged in little-endian on windows
                msglen = BitConverter.ToUInt16(new byte[] { requestBin[3], requestBin[2] }, 0);
                offset = 4;
            }

            if (mask)
            {
                byte[] decoded = new byte[msglen];
                byte[] masks = new byte[4]
                {
                    requestBin[offset],
                    requestBin[offset + 1],
                    requestBin[offset + 2],
                    requestBin[offset + 3]
                };
                offset += 4;

                for (ulong i = 0; i < msglen; ++i)
                    decoded[i] = (byte)(requestBin[offset + i] ^ masks[i % 4]);

                return Encoding.UTF8.GetString(decoded);
            }
            else
            {
                ShowDebug("Bad request: mask bit not set");
                return string.Empty;
            }
        }

        private static bool IsSocketConnected(Socket s) => !(s.Poll(1000, SelectMode.SelectRead) && s.Available == 0);

        private static void ShowError(Exception ex) => Console.WriteLine($"\nEXCEPTION: {ex.ToString()}\n");

        private static void ShowDebug(string s, string? owner = null)
        {
            if (_isDebug)
                Console.WriteLine(owner == null ? $"\nDEBUG: {s}\n" : $"\nDEBUG: {owner}: {s}\n");
        }
    }
}
