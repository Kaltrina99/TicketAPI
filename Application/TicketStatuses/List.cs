using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.TicketStatuses
{
    public class List
    {
        public class Query : IRequest<List<TicketStatus>> { }

        public class Handler : IRequestHandler<Query, List<TicketStatus>>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<List<TicketStatus>> Handle(Query request, CancellationToken cancellationToken)
            {
                var ticketStatus= await _context.TicketStatuses.ToListAsync();
                return ticketStatus;
            }
        }
    }
}