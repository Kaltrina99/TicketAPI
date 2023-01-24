using System;
using System.Collections.Generic;

namespace Domain
{
    public class UserTicket
    {
        public string UserId { get; set; }
        public Guid TicketId { get; set; }

        public virtual AppUser User { get; set; }
        public virtual Ticket Ticket { get; set; }
        public DateTime DateJoined { get; set; }
        public bool IsHost { get; set; }
    }
}