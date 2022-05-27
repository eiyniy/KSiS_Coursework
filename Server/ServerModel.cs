// SocketServer.cs
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Concurrent;

namespace Server
{
    public static class ServerModel
    {
        private static Socket _sListener;

        private static ConcurrentQueue<Socket> clientsHandlers = new ConcurrentQueue<Socket>();


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
                Console.WriteLine(ex.ToString());
                return;
            }
            finally
            {
                foreach (var handler in clientsHandlers)
                    CloseClient(handler);
            }
        }

        private static void AcceptNewClients(IPEndPoint ipEndPoint)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("L: Ожидаем соединение через порт {0}", ipEndPoint);

                    Socket handler = _sListener.Accept();
                    Console.WriteLine("L: Handler created");

                    clientsHandlers.Enqueue(handler);

                    Task.Run(() => ProcessClientRequests(handler));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }
        }

        private static void ProcessClientRequests(Socket handler)
        {
            try
            {
                Console.WriteLine("P1: Processing started");

                while (true)
                {
                    byte[] clientBinRequest = new byte[1024];
                    int bytesRec = handler.Receive(clientBinRequest);

                    string clientRequest = Encoding.UTF8.GetString(clientBinRequest, 0, bytesRec);

                    if (clientRequest.IndexOf("--exit") > -1)
                        throw new Exception("P1: Сервер завершил соединение с клиентом.");

                    // Показываем данные на консоли
                    Console.Write("P1: Полученный текст: " + clientRequest + "\n\n");

                    // Отправляем ответ клиенту
                    string reply = $"P1: Спасибо за запрос в {clientRequest.Length} символов";
                    byte[] msg = Encoding.UTF8.GetBytes(reply);
                    handler.Send(msg);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                CloseClient(handler);
                return;
            }
        }

        private static void CloseClient(Socket handler)
        {
            try
            {
                string reply = "--exit";
                byte[] msg = Encoding.UTF8.GetBytes(reply);
                handler.Send(msg);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
                Console.WriteLine("L: Handler closed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }
        }
    }
}
