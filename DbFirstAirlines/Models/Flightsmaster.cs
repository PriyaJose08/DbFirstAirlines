using System;
using System.Collections.Generic;

namespace DbFirstAirlines.Models
{
    public partial class Flightsmaster
    {
        public Flightsmaster()
        {
            Bookings = new HashSet<Booking>();
            Flights = new HashSet<Flight>();
            Seats = new HashSet<Seat>();
        }

        public int FlightregistrationId { get; set; }
        public string? Flightname { get; set; }
        public decimal? Economyseats { get; set; }
        public decimal? Businessseats { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Flight> Flights { get; set; }
        public virtual ICollection<Seat> Seats { get; set; }
    }
}
