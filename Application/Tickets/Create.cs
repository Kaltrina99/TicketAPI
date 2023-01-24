using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Tickets
{
    public class Create
    {
       public class Command : IRequest
        {
           [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
            private readonly IUserAccessor _userAccessor;
            public Handler(ticketContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;

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
               var ticket=new Ticket
               {
                    Id=request.Id,
                    Subject=request.Subject,
                    Issue=request.Issue,
                    IsDeleted=request.IsDeleted,
                    DueDate=request.DueDate,
                    ClosedDate=request.ClosedDate,
                    CreatedDate=request.CreatedDate,
                    UpdatedDate=request.UpdatedDate   
               };

               _context.Tickets.Add(ticket);
                var user= await _context.Users.SingleOrDefaultAsync(x=>x.UserName==_userAccessor.GetCurrentUsername());

                var data=new UserTicket
                {
                    User=user,
                    Ticket=ticket,
                    IsHost=true,
                    DateJoined=DateTime.Now
                };
                _context.UserTickets.Add(data);
               var success=await _context.SaveChangesAsync()>0;
               if(success) return Unit.Value;

               throw new Exception("Problem saving the data for the company");
            }
        }
    }
}