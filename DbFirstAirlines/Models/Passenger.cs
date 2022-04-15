using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbFirstAirlines.Models
{
    public partial class Passenger
    {
        public Passenger()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int BookingId { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        public int? SeatId { get; set; }
        [Required]
        public string? ClassType { get; set; }
        public int PassengerId { get; set; }

        public virtual Booking Booking { get; set; } = null!;
        public virtual Seat? Seat { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
