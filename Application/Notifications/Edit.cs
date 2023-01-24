using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Notifications
{
    public class Edit
    {
          public class Command : IRequest
        {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public bool Unread { get; set; }
        public DateTime CreatedDate { get; set; }
        }
        
        public class Handler : IRequestHandler<Command>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;
        
            }
            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(x=>x.Title).NotEmpty();
                    RuleFor(x=>x.Message).NotEmpty();
                    RuleFor(x=>x.Type).NotEmpty();
                }
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var notification = await _context.Notifications.FindAsync(request.Id);
                if(notification==null)
                       throw new RestException(HttpStatusCode.NotFound,new {notification="Could not find"});
                    
                notification.Title=request.Title??notification.Title;
                notification.Message=request.Message??notification.Message;
                notification.Type=request.Type??notification.Type;
                notification.Unread=request.Unread;
                notification.CreatedDate=notification.CreatedDate;

                var success=await _context.SaveChangesAsync()>0;
                if(success) return Unit.Value;
                throw new Exception("Problem editing the data");

            }
        }
    }
}