using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.TicketTypes
{
    public class Edit
    {
        public class Command : IRequest
        {
           public Guid Id { get; set; }
           public string Name { get; set; }
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
                }
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var ticketType = await _context.TicketTypes.FindAsync(request.Id);
                if(ticketType==null)
                       throw new RestException(HttpStatusCode.NotFound,new {ticketType="Could not find"});
                    
                ticketType.Name=request.Name??ticketType.Name;
                ticketType.CreatedDate=ticketType.CreatedDate;
                ticketType.UpdatedDate=request.UpdatedDate;


                var success=await _context.SaveChangesAsync()>0;
                if(success) return Unit.Value;
                throw new Exception("Problem editing the data");

            }
        }
    }
}