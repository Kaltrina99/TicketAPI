using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Notes
{
    public class Edit
    {
          public class Command : IRequest
        {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
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
                    RuleFor(x=>x.Description).NotEmpty();
                }
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var note = await _context.Notes.FindAsync(request.Id);
                if(note==null)
                       throw new RestException(HttpStatusCode.NotFound,new {note="Could not find"});
                    
                note.Description=request.Description??note.Description;
                note.IsDeleted=request.IsDeleted;
                note.CreatedDate=note.CreatedDate;
                note.UpdatedDate=request.UpdatedDate;


                var success=await _context.SaveChangesAsync()>0;
                if(success) return Unit.Value;
                throw new Exception("Problem editing the data");

            }
        }
    }
}