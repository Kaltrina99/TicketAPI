using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Histories
{
    public class Edit
    {
          public class Command : IRequest
        {
       public Guid Id { get; set; }
        public string Action { get; set; }
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
                    RuleFor(x=>x.Action).NotEmpty();
                }
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var tags = await _context.Histories.FindAsync(request.Id);
                if(tags==null)
                       throw new RestException(HttpStatusCode.NotFound,new {tags="Could not find"});
                    
                tags.Action=request.Action??tags.Action;
                tags.CreatedDate=tags.CreatedDate;


                var success=await _context.SaveChangesAsync()>0;
                if(success) return Unit.Value;
                throw new Exception("Problem editing the data");

            }
        }
    }
}