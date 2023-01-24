using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Teams
{
    public class Create
    {
           public class Command : IRequest
        {
           [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
               var team=new Team
               {
                    Id=request.Id,
                    Name=request.Name,
                    CreatedDate=request.CreatedDate,
                    UpdatedDate=request.UpdatedDate   
               };

               _context.Teams.Add(team);
               var success=await _context.SaveChangesAsync()>0;
               if(success) return Unit.Value;

               throw new Exception("Problem saving the data for the company");
            }
        }
    }
}