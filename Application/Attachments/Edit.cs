using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Attachments
{
    public class Edit
    {
          public class Command : IRequest
        {
         public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
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
                    RuleFor(x=>x.Path).NotEmpty();
                    RuleFor(x=>x.Type).NotEmpty();

                }
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var attachment = await _context.Attachments.FindAsync(request.Id);
                if(attachment==null)
                       throw new RestException(HttpStatusCode.NotFound,new {attachment="Could not find"});
                    
                attachment.Name=request.Name??attachment.Name;
                attachment.Path=request.Path??attachment.Path;
                attachment.Type=request.Type??request.Type;
                attachment.CreatedDate=attachment.CreatedDate;
                attachment.UpdatedDate=request.UpdatedDate;


                var success=await _context.SaveChangesAsync()>0;
                if(success) return Unit.Value;
                throw new Exception("Problem editing the data");

            }
        }
    }
}