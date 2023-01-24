using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.TicketStatuses
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
                var ticketStatus = await _context.TicketStatuses.FindAsync(request.Id);
                if(ticketStatus==null)
                       throw new RestException(HttpStatusCode.NotFound,new {ticketStatus="Could not find"});
                    
                ticketStatus.Name=request.Name??ticketStatus.Name;
                ticketStatus.IsDefault=request.IsDefault;
                ticketStatus.Color=request.Color??ticketStatus.Color;
                ticketStatus.CreatedDate=ticketStatus.CreatedDate;
                ticketStatus.UpdatedDate=request.UpdatedDate;


                var success=await _context.SaveChangesAsync()>0;
                if(success) return Unit.Value;
                throw new Exception("Problem editing the data");

            }
        }
    }
}