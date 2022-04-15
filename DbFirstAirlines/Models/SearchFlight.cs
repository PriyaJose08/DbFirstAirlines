using System.ComponentModel.DataAnnotations;

namespace DbFirstAirlines.Models
{
    public class SearchFlight
    {
        [Required]
        public string? SourceArea { get; set; }

        [Required]
        public string? DestinationArea { get; set; }
        [Required]

        public DateTime? DateofJourney { get; set; }

    }
}
