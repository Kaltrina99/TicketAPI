using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Teams
{
    public class List
    {
        public class Query : IRequest<List<Team>> { }

        public class Handler : IRequestHandler<Query, List<Team>>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<List<Team>> Handle(Query request, CancellationToken cancellationToken)
            {
                var teams= await _context.Teams.ToListAsync();
                return teams;
            }
        }
    }
}