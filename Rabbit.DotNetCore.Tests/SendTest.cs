namespace Rabbit.DotNetCore.Tests
{
    using System.Text;
    using System.Threading;
    using NUnit.Framework;
    using RabbitMQ.Client;

    [TestFixture]
    public class SendTest
    {
        [Test]
        public void SendMessage()
        {
            uint messageCount = 0;
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "test", durable: false, exclusive: false, autoDelete: false, arguments: null);

                const string message = "hello_world";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: "test", basicProperties: null, body: body);
                Thread.Sleep(500);
                messageCount = channel.MessageCount("test");
            }

            Assert.That(messageCount, Is.EqualTo(1));
        }
    }
}
