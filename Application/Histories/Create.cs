using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Histories
{
    public class Create
    {
           public class Command : IRequest
        {
           [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
               var history=new History
               {
                    Id=request.Id,
                    Action=request.Action,
                    CreatedDate=request.CreatedDate,
               };

               _context.Histories.Add(history);
               var success=await _context.SaveChangesAsync()>0;
               if(success) return Unit.Value;

               throw new Exception("Problem saving the data for the company");
            }
        }
    }
}