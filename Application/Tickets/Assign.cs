using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Tickets
{
    public class Assign
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ticketContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(ticketContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var ticket= await _context.Tickets.FindAsync(request.Id);

                if(ticket==null)
                    throw new RestException(HttpStatusCode.NotFound, new {Ticket="Could not find ticket"});

                var user = await _context.Users.SingleOrDefaultAsync(x=>x.UserName== _userAccessor.GetCurrentUsername());

                var assign=await _context.UserTickets.SingleOrDefaultAsync(x=>x.TicketId==ticket.Id && x.UserId==user.Id);

                if(assign!=null)
                    throw new RestException(HttpStatusCode.BadRequest, new {Assign="Already assign to this ticket"});

                assign=new UserTicket{
                    Ticket=ticket,
                    User=user,
                    IsHost=false,
                    DateJoined=DateTime.Now
                };

                _context.UserTickets.Add(assign);

                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;
                throw new Exception("Problem saving the data");
            }
        }
    }
}