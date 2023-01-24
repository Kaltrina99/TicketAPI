using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class ticketContext: IdentityDbContext<AppUser, AppRole, string>
    {
        public ticketContext(DbContextOptions options):base(options)
        {
        }
        public DbSet<Value> Values { get; set; }
        public DbSet<Company> Companies  { get; set; }
        public DbSet<TicketType> TicketTypes  { get; set; }
        public DbSet<TicketPriority> TicketPriorities { get; set; }
        public DbSet<TicketStatus> TicketStatuses  { get; set; }
        public DbSet<Ticket> Tickets  { get; set; }
        public DbSet<Report> Reports  { get; set; }
        public DbSet<Tag> Tags  { get; set; }
        public DbSet<History> Histories  { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Comment> Comments  { get; set; }
        public DbSet<Note> Notes  { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Department> Departments  { get; set; }
        public DbSet<Notification> Notifications  { get; set; }

        public DbSet<UserTicket> UserTickets  { get; set; }

        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);
            builder.Entity<Value>()
            .HasData(
                new Value{Id=1, Name="103"},
                new Value{Id=2, Name="102"},
                new Value{Id=3, Name="101"}
            );

            builder.Entity<UserTicket>(x=>x.HasKey(ua=>new {ua.UserId,ua.TicketId}));
            builder.Entity<UserTicket>().HasOne(u=>u.User).WithMany(t=>t.UserTickets).HasForeignKey(u=>u.UserId);
            builder.Entity<UserTicket>().HasOne(t=>t.Ticket).WithMany(u=>u.UserTickets).HasForeignKey(t=>t.TicketId);
        }
    }
}