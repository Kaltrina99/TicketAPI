using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Role
{
    public class Edit
    {
         public class Command : IRequest
        {
            public string Id { get; set; }
           public string Name { get; set; }
           public string NormalizedName { get; set; }
           public bool IsStatic { get; set; }
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
                    RuleFor(x=>x.NormalizedName).NotEmpty();

                }
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var role = await _context.Roles.FindAsync(request.Id);
                if(role==null)
                       throw new RestException(HttpStatusCode.NotFound,new {role="Could not find"});
                    
                role.Name=request.Name??role.Name;
                role.NormalizedName=request.NormalizedName??role.NormalizedName;
                role.IsStatic=request.IsStatic;


                var success=await _context.SaveChangesAsync()>0;
                if(success) return Unit.Value;
                throw new Exception("Problem editing the data");

            }

        }
    }
}