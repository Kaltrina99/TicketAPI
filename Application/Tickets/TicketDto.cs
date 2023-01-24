using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Tickets
{
    public class TicketDto
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Issue { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        [JsonPropertyName("User")]
        public ICollection<AssignedDto> Assigneds { get; set; }
    }
}