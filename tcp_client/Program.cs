using System;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace tcp_client
{
    class Program
    {
        // адрес и порт сервера, к которому будем подключаться
        static int port = 8080; // порт сервера
        static string address = "127.0.0.1"; // адрес сервера
        static void Main(string[] args)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
            TcpClient client = new TcpClient();

            // подключаемся к удаленному хосту
            client.Connect(ipPoint);

            string message = "";
            try
            {
                while (message != "end")
                {
                    Console.Write("Enter a message:");
                    message = Console.ReadLine();

                    NetworkStream ns = client.GetStream();

                    StreamWriter sw = new StreamWriter(ns);
                    sw.WriteLine(message);//Hello


                    sw.Flush(); // send all buffered data and clear the buffer // 1Kb

                    // получаем ответ

                    StreamReader sr = new StreamReader(ns);
                    string response = sr.ReadLine();

                    Console.WriteLine("server response: " + response);

                    // закриваємо потокі
                    //sw.Close();
                    //sr.Close();
                    //ns.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // закрываем сокет
                client.Close();
            }
        }
    }
}
