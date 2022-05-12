using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQ.Producer2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("I am Producer!");
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare("input-message-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            while (true)
            {
                string ipMessage = Console.ReadLine().ToString();
                if (ipMessage.ToUpper()!="EXIT")
                {
                    var message = new { Name = "Producer", Message = ipMessage };
                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                    channel.BasicPublish("", "input-message-queue", null, body);
                    Console.WriteLine("Input Message Sent");
                }
                else
                {
                    break;
                }               
            }

            channel.Close();
            connection.Close();
        }
    }
}