using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;
using RabbitMQ.API.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace RabbitMQ.API.Services;

public class MessageProducer : IMessageProducer
{
    public async Task<List<Message>> ReceiveMessage()
    {
        var allMessages = new List<Message>();
        var tcs = new TaskCompletionSource<List<Message>>();
        var factory = new ConnectionFactory()
        {   
            HostName = "localhost",
            UserName = "user",
            Password = "password"
        };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "MyQueue",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            allMessages.Add(new Message { Content = message});

        };

        channel.BasicConsume(queue: "MyQueue",
                            autoAck: true,
                            consumer: consumer);
            
            await Task.Delay(1000);
            tcs.SetResult(allMessages);
        }

        return await tcs.Task;
    }
    public void SendMessage(string message)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "user",
            Password = "password"
        };
        
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {   
            channel.QueueDeclare(queue: "MyQueue",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
                                routingKey: "MyQueue",
                                basicProperties: null,
                                body: body);
        }
    }
}
