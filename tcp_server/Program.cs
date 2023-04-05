using ShareData;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace tcp_server
{
    class Program
    {
        static int port = 8080; // порт для приема входящих запросов
        static void Main(string[] args)
        {
            // получаем адреса для запуска сокета
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");//Dns.GetHostEntry("localhost").AddressList[1]; //localhost
            IPEndPoint ipPoint = new IPEndPoint(iPAddress, port);
            TcpListener listener = new TcpListener(ipPoint); // bind


            // связываем сокет с локальной точкой, по которой будем принимать данные

            // начинаем прослушивание
            listener.Start(10);

            Console.WriteLine("Server started! Waiting for connection...");
            TcpClient client = listener.AcceptTcpClient();
            try
            {

                while (client.Connected)
                {

                    NetworkStream ns = client.GetStream();

                    BinaryFormatter formatter = new BinaryFormatter();
                    var request = (Request)formatter.Deserialize(ns);

                    Console.WriteLine($"Request data : {request.A} {request.B} from {client.Client.LocalEndPoint}");

                    double res = 0;
                    switch (request.OperationType)
                    {
                        case OperationType.Add:
                            res = request.A + request.B;
                            break;
                        case OperationType.Sub:
                            res = request.A - request.B;
                            break;
                        case OperationType.Mult:
                            res = request.A * request.B;
                            break;
                        case OperationType.Div:
                            res = request.A / request.B;
                            break;
                    }

                    // отправляем ответ
                    string message = $"Result = {res}";
                    StreamWriter sw = new StreamWriter(ns);
                    sw.WriteLine(message);
                    sw.Flush();
                    // закриваємо потокі
                    //sr.Close();
                    //sw.Close();
                    //ns.Close();
                }
                // закрываем сокет
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            listener.Stop();
        }
    }
}
