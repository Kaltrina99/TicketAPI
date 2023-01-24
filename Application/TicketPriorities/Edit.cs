using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.TicketPriorities
{
    public class Edit
    {
          public class Command : IRequest
        {
           public Guid Id { get; set; }
           public string Name { get; set; }
           public string Color { get; set; }
           public bool IsDefault { get; set; }
           public DateTime CreatedDate { get; set; }
           public DateTime UpdatedDate { get; set; }
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
                    RuleFor(x=>x.Name).NotEmpty();
                    RuleFor(x=>x.Color).NotEmpty();

                }
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var ticketPriority = await _context.TicketPriorities.FindAsync(request.Id);
                if(ticketPriority==null)
                       throw new RestException(HttpStatusCode.NotFound,new {ticketPriority="Could not find"});
                    
                ticketPriority.Name=request.Name??ticketPriority.Name;
                ticketPriority.IsDefault=request.IsDefault;
                ticketPriority.Color=request.Color??ticketPriority.Color;
                ticketPriority.CreatedDate=ticketPriority.CreatedDate;
                ticketPriority.UpdatedDate=request.UpdatedDate;


                var success=await _context.SaveChangesAsync()>0;
                if(success) return Unit.Value;
                throw new Exception("Problem editing the data");

            }
        }
    }
}