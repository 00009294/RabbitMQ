using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitMQ.API.Services;

    public class MessageProducer : IMessageProducer

    {
        public void SendingMessage<T>(T messsage)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "myPassword",
                VirtualHost = "/"
            };

            var conn = factory.CreateConnection();
            
            using var channel = conn.CreateModel();
            channel.QueueDeclare(queue: "booking", durable: true, exclusive: true);
            
            var jsonString = JsonSerializer.Serialize(messsage);
            var body = Encoding.UTF8.GetBytes(jsonString);

            channel.BasicPublish("", "booking", body: body);
    }
}
