using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Departments
{
    public class Edit
    {
          public class Command : IRequest
        {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Normalized { get; set; }
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
                    RuleFor(x=>x.Normalized).NotEmpty();

                }
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var department = await _context.Departments.FindAsync(request.Id);
                if(department==null)
                       throw new RestException(HttpStatusCode.NotFound,new {department="Could not find"});
                    
                department.Name=request.Name??department.Name;
                department.Normalized=request.Normalized??department.Normalized;
                department.CreatedDate=department.CreatedDate;
                department.UpdatedDate=request.UpdatedDate;


                var success=await _context.SaveChangesAsync()>0;
                if(success) return Unit.Value;
                throw new Exception("Problem editing the data");

            }
        }
    }
}