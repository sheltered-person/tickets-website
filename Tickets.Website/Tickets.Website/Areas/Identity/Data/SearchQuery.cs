using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tickets.Website.Areas.Identity.Data
{
    public class SearchQuery
    {
        public string DepartureAirport { get; set; }

        public string ArrivalAirport { get; set; }

        public DateTime DepartureDate { get; set; }

        public char Class { get; set; }
    }
}
