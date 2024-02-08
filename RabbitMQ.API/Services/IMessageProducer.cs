using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.API.Models;

namespace RabbitMQ.API.Services
{
    public interface IMessageProducer
    {
	    void SendMessage(string message);
        Task<List<Message>> ReceiveMessage();
    }
}