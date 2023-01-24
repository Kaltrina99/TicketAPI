using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Histories
{
    public class List
    {
        public class Query : IRequest<List<History>> { }

        public class Handler : IRequestHandler<Query, List<History>>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<List<History>> Handle(Query request, CancellationToken cancellationToken)
            {
                var histories= await _context.Histories.ToListAsync();
                return histories;
            }
        }
    }
}