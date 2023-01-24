using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.TicketPriorities
{
    public class Create
    {
           public class Command : IRequest
        {
           [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
               var ticketPriority=new TicketPriority
               {
                    Id=request.Id,
                    Name=request.Name,
                    Color=request.Color,
                    IsDefault=request.IsDefault,
                    CreatedDate=request.CreatedDate,
                    UpdatedDate=request.UpdatedDate   
               };

               _context.TicketPriorities.Add(ticketPriority);
               var success=await _context.SaveChangesAsync()>0;
               if(success) return Unit.Value;

               throw new Exception("Problem saving the data for the company");
            }
        }
    }
}