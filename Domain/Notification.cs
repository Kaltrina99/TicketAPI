using System;

namespace Domain
{
    public class Notification
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public bool Unread { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}