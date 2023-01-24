using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Tickets
{
    public class Edit
    {
          public class Command : IRequest
        {
         public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Issue { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public DateTime? DueDate { get; set; }
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
                    RuleFor(x=>x.Subject).NotEmpty();
                    RuleFor(x=>x.Issue).NotEmpty();

                }
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var ticket = await _context.Tickets.FindAsync(request.Id);
                if(ticket==null)
                       throw new RestException(HttpStatusCode.NotFound,new {ticket="Could not find"});
                    
                ticket.Subject=request.Subject??ticket.Subject;
                ticket.Issue=request.Issue??ticket.Issue;
                ticket.IsDeleted=request.IsDeleted;
                ticket.DueDate=ticket.DueDate??request.DueDate;
                ticket.ClosedDate=request.ClosedDate??ticket.ClosedDate;
                ticket.CreatedDate=ticket.CreatedDate;
                ticket.UpdatedDate=request.UpdatedDate;

                var success=await _context.SaveChangesAsync()>0;
                if(success) return Unit.Value;
                throw new Exception("Problem editing the data");

            }
        }
    }
}