using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.TicketStatuses
{
    public class Details
    {
        public class Query : IRequest<TicketStatus>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, TicketStatus>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<TicketStatus> Handle(Query request, CancellationToken cancellationToken)
            {
                var ticketStatus = await _context.TicketStatuses.FindAsync(request.Id);
                if (ticketStatus == null)
                    throw new RestException(HttpStatusCode.NotFound, new { ticketStatus = "Could not find" });
                return ticketStatus;
            }
        }
    }
}