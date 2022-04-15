using System;
using System.Collections.Generic;

namespace DbFirstAirlines.Models
{
    public partial class Flight
    {
        public Flight()
        {
            Bookings = new HashSet<Booking>();
        }

        public int JourneyId { get; set; }
        public int? Flightregistrationid { get; set; }
        public string? SourceArea { get; set; }
        public string? DestinationArea { get; set; }
        public DateTime? DateofJourney { get; set; }
        public DateTime? Departuretime { get; set; }
        public TimeSpan? Arrivaltime { get; set; }
        public decimal? Businessclassprice { get; set; }
        public decimal? Economyclassprice { get; set; }
        public int? Duration { get; set; }
        public int? Availableeconomyseats { get; set; }
        public int? Availablebusinessseats { get; set; }

        public virtual Flightsmaster? Flightregistration { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
