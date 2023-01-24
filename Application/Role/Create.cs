using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Role
{
    public class Create
    {
         public class Command : IRequest
        {
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
               var role=new AppRole
               {
                    Name=request.Name,
                    NormalizedName=request.NormalizedName,
                    IsStatic=request.IsStatic,
               };

               _context.Roles.Add(role);
               var success=await _context.SaveChangesAsync()>0;
               if(success) return Unit.Value;

               throw new Exception("Problem saving the data for the role");
            }

        }
    }
}