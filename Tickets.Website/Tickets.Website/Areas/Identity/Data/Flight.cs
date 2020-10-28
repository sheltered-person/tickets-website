using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tickets.Website.Areas.Identity.Data
{
    public class Flight
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime DepartureDate { get; set; }

        [Required]
        public DateTime ArrivalDate { get; set; }

        [Required]
        public string AviaCompany { get; set; }

        [Required]
        public string PlaneInformation { get; set; }

        [Required]
        [ForeignKey("Route")]
        public int RouteId { get; set; }

        public Route Route { get; set; }
    }
}
