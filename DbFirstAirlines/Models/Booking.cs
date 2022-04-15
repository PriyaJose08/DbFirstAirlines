using System;
using System.Collections.Generic;

namespace DbFirstAirlines.Models
{
    public partial class Booking
    {
        public Booking()
        {
            Passengers = new HashSet<Passenger>();
            Seats = new HashSet<Seat>();
            Tickets = new HashSet<Ticket>();
        }

        public int? JourneyId { get; set; }
        public int? FlightRegistrationId { get; set; }
        public int BookingId { get; set; }
        public int? NoOfPassengers { get; set; }

        public virtual Flightsmaster? FlightRegistration { get; set; }
        public virtual Flight? Journey { get; set; }
        public virtual ICollection<Passenger> Passengers { get; set; }
        public virtual ICollection<Seat> Seats { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
