namespace Rabbit.DotNetCore.Tests
{
    using System.Text;
    using NUnit.Framework;
    using RabbitMQ.Client;

    [TestFixture]
    public class ReceiveTests
    {
        [Test]
        public void ReceiveMessage()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // First message
                var message = channel.BasicGet("test", autoAck: false);

                Assert.That(message, Is.Not.Null);
                var messageBody = Encoding.ASCII.GetString(message.Body);

                Assert.That(messageBody, Is.EqualTo("hello_world"));

                channel.BasicAck(message.DeliveryTag, multiple: false);
            }
        }
    }
}