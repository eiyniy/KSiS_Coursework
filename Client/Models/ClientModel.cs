using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client.Models
{
    public static class ClientModel
    {
        private static Socket _sender;

        private static byte[] _bytes = new byte[1024];


        public static void Connect(int port)
        {
            try
            {
                IPHostEntry ipHost = Dns.GetHostEntry("localhost");
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

                _sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // Соединяем сокет с удаленной точкой
                _sender.Connect(ipEndPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static string SendMessageToServer(string message)
        {
            try
            {
                Console.WriteLine($"Сокет соединяется с {_sender.RemoteEndPoint.ToString()} ");
                byte[] msg = Encoding.UTF8.GetBytes(message);

                // Отправляем данные через сокет
                int bytesSent = _sender.Send(msg);
                Console.WriteLine($"Сообщение доставлено");

                // Получаем ответ от сервера
                int bytesRec = _sender.Receive(_bytes);
                string answer = Encoding.UTF8.GetString(_bytes, 0, bytesRec);

                Console.WriteLine("\nОтвет от сервера: {0}\n\n", answer);

                if (message.IndexOf("--exit") > -1)
                {
                    Console.WriteLine("Клиент завершил соединение с сервером.");
                    Disconnect();
                    return "DISCONNECTED";
                }
                else
                    return answer;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "ERROR";
            }
        }

        public static void Disconnect()
        {
            try
            {
                // Освобождаем сокет
                _sender.Shutdown(SocketShutdown.Both);
                _sender.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
