using System;
using System.Collections.Generic;

namespace DbFirstAirlines.Models
{
    public partial class Ticket
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int SeatId { get; set; }
        public int BookingId { get; set; }
        public int PassengerId { get; set; }
        public int TicketId { get; set; }

        public virtual Booking Booking { get; set; } = null!;
        public virtual Passenger? Passenger { get; set; }
        public virtual Seat Seat { get; set; } = null!;
    }
}
