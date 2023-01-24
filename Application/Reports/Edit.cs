using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Reports
{
    public class Edit
    {
          public class Command : IRequest
        {
        public Guid Id { get; set; }
        public string Name { get; set; }
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
                    RuleFor(x=>x.Type).NotEmpty();

                }
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var report = await _context.Reports.FindAsync(request.Id);
                if(report==null)
                       throw new RestException(HttpStatusCode.NotFound,new {report="Could not find"});
                    
                report.Name=request.Name??report.Name;
                report.Type=request.Type??report.Type;
                report.CreatedDate=report.CreatedDate;
                report.UpdatedDate=request.UpdatedDate;


                var success=await _context.SaveChangesAsync()>0;
                if(success) return Unit.Value;
                throw new Exception("Problem editing the data");

            }
        }
    }
}