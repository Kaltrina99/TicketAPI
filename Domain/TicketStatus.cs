using System;

namespace Domain
{
    public class TicketStatus
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public bool IsDefault { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}