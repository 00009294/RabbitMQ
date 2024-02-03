using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.API.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string PassengerName { get; set; } = String.Empty;
        public string PassportNumber { get; set; } = String.Empty;
        public string From { get; set; } = String.Empty;
        public string To { get; set; } = String.Empty;
        public int Status { get; set;} 
    }
}