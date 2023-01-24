using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.TicketPriorities
{
    public class List
    {
        public class Query : IRequest<List<TicketPriority>> { }

        public class Handler : IRequestHandler<Query, List<TicketPriority>>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<List<TicketPriority>> Handle(Query request, CancellationToken cancellationToken)
            {
                var ticketPriorities= await _context.TicketPriorities.ToListAsync();
                return ticketPriorities;
            }
        }
    }
}