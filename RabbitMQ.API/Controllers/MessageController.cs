using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.API.Models;
using RabbitMQ.API.Services;

namespace RabbitMQ.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageProducer messageProducer;

        public MessageController(IMessageProducer messageProducer)
        {
            this.messageProducer = messageProducer;
        }

        [HttpPost]
        public IActionResult SendMessage([FromBody]string message)
        {
            this.messageProducer.SendMessage(message);

		    return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<Message>>> ReceiveMessage()
        {
            var res = await this.messageProducer.ReceiveMessage();

            return  Ok(res);
        }
    }
}