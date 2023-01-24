using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.TicketPriorities
{
    public class Details
    {
        public class Query : IRequest<TicketPriority>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, TicketPriority>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<TicketPriority> Handle(Query request, CancellationToken cancellationToken)
            {
                var ticketPriority = await _context.TicketPriorities.FindAsync(request.Id);
                if (ticketPriority == null)
                    throw new RestException(HttpStatusCode.NotFound, new { ticketPriority = "Could not find" });
                return ticketPriority;
            }
        }
    }
}