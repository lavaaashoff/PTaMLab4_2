using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Controller
{
    internal class Program
    {
        private const int Port = 5000;
        private const double MinTemperature = 0.0;
        private const double MaxTemperature = 100.0;
        private const double MinPressure = 0.0;
        private const double MaxPressure = 6.0;

        private static readonly Random Random = new Random();

        static void Main(string[] args)
        {
            Console.Title = "Контроллер технологического процесса";
            Console.WriteLine("=== Контроллер технологического процесса ===");
            Console.WriteLine($"Порт: {Port}");
            Console.WriteLine("Ожидание подключения диспетчера...");
            Console.WriteLine("Нажмите Ctrl+C для завершения.");
            Console.WriteLine();

            TcpListener listener = new TcpListener(IPAddress.Any, Port);
            listener.Start();

            while (true)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Диспетчер подключился: {client.Client.RemoteEndPoint}");

                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка приёма подключения: {ex.Message}");
                }
            }
        }

        private static void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            try
            {
                while (client.Connected)
                {
                    double temperature = GenerateValue(MinTemperature, MaxTemperature);
                    double pressure = GenerateValue(MinPressure, MaxPressure);

                    string message = string.Format(
                        CultureInfo.InvariantCulture,
                        "{0:F2};{1:F2}\n",
                        temperature,
                        pressure);
                    byte[] data = Encoding.UTF8.GetBytes(message);

                    stream.Write(data, 0, data.Length);

                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Отправлено -> Температура: {temperature:F2}°C, Давление: {pressure:F2} атм");

                    Thread.Sleep(1000);
                }
            }
            catch (Exception)
            {
                // Клиент отключился
            }
            finally
            {
                stream.Close();
                client.Close();
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Диспетчер отключился.");
            }
        }

        private static double GenerateValue(double min, double max)
        {
            return min + (max - min) * Random.NextDouble();
        }
    }
}