using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Notifications
{
    public class Create
    {
           public class Command : IRequest
        {
           [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
               var notification=new Notification
               {
                    Id=request.Id,
                    Title=request.Title,
                    Message=request.Message,
                    Type=request.Type,
                    Unread=request.Unread,
                    CreatedDate=request.CreatedDate   
               };

               _context.Notifications.Add(notification);
               var success=await _context.SaveChangesAsync()>0;
               if(success) return Unit.Value;

               throw new Exception("Problem saving the data for the company");
            }
        }
    }
}