using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.TicketTypes
{
    public class List
    {
        public class Query : IRequest<List<TicketType>> { }

        public class Handler : IRequestHandler<Query, List<TicketType>>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<List<TicketType>> Handle(Query request, CancellationToken cancellationToken)
            {
                var ticketTypes= await _context.TicketTypes.ToListAsync();
                return ticketTypes;
            }
        }
    }
}