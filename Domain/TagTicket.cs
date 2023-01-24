using System;
using System.Collections.Generic;

namespace Domain
{
    public class TagTicket
    {
        public Guid TagId { get; set; }
        public Guid TicketId { get; set; }

        public virtual ICollection<Ticket> Ticket { get; set; }
        public virtual ICollection<Tag> Tag { get; set; }
    }
}