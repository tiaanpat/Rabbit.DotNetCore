namespace Rabbit.DotNetCore.Send
{
    using System;
    using System.Text;
    using RabbitMQ.Client;

    class Send
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
                string message = string.Empty;
                byte[] body;
                if (args.Length == 0)
                {
                    Console.WriteLine("Enter your name: ");
                    message = Console.ReadLine();
                    body = Encoding.UTF8.GetBytes(message);
                }
                else
                {
                    message = args[args.Length - 1].ToString();
                    body = Encoding.UTF8.GetBytes(message);
                }

                channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
                Console.WriteLine($"Hello my name is, {message}");
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
