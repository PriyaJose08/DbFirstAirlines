using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbFirstAirlines.Models
{
    public partial class Seat
    {
        public Seat()
        {
            Passengers = new HashSet<Passenger>();
            Tickets = new HashSet<Ticket>();
        }

        public int? FlightregistrationId { get; set; }
        public int? BookingId { get; set; }
        [Required]
        public int SeatId { get; set; }

        public virtual Booking? Booking { get; set; }
        public virtual Flightsmaster? Flightregistration { get; set; }
        public virtual ICollection<Passenger> Passengers { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
