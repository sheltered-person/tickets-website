using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Tickets.Website.Areas.Identity.Data
{
    public class TicketPerson
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Person")]
        public int PersonId { get; set; }

        public Person Person { get; set; }

        [Required]
        [ForeignKey("Ticket")]
        public int TicketId { get; set; }

        public Ticket Ticket { get; set; }
    }
}
