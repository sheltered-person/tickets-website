using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tickets.Website.Areas.Identity.Data
{
    public class Ticket
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        public char Class { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public bool IsHandLuggageIncluded { get; set; }

        [Required]
        public bool IsLuggageIncluded { get; set; }

        [Required]
        public bool AvailabilityStatus { get; set; }

        [Required]
        [ForeignKey("Flight")]
        public int FlightId { get; set; }

        public Flight Flight { get; set; }
    }
}
