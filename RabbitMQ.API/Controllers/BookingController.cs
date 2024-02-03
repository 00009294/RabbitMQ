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
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> logger;
        private readonly IMessageProducer messageProducer;

        /// <summary>
        /// In-memory database
        /// </summary>
        public static readonly List<Booking> bookings = [];

        public BookingController(
            ILogger<BookingController> logger, 
            IMessageProducer messageProducer)
        {
            this.logger = logger;
            this.messageProducer = messageProducer;
        }

        [HttpPost]
        public IActionResult CreateBooking(Booking newBooking)
        {
            if(!ModelState.IsValid) return BadRequest();

            bookings.Add(newBooking);
            this.messageProducer.SendingMessage<Booking>(newBooking);

            return Ok();
        }
    }
}