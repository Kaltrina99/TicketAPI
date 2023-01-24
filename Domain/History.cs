using System;

namespace Domain
{
    public class History
    {
        public Guid Id { get; set; }
        public string Action { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}