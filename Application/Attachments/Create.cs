using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Attachments
{
    public class Create
    {
       public class Command : IRequest
        {
           [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
               var attachment=new Attachment
               {
                    Id=request.Id,
                    Name=request.Name,
                    Path=request.Path,
                    Type=request.Type,
                    CreatedDate=request.CreatedDate,
                    UpdatedDate=request.UpdatedDate   
               };

               _context.Attachments.Add(attachment);
               var success=await _context.SaveChangesAsync()>0;
               if(success) return Unit.Value;

               throw new Exception("Problem saving the data for the company");
            }
        }
    }
}