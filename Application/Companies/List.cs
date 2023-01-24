using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Companies
{
    public class List
    {
        public class Query : IRequest<List<Company>> { }

        public class Handler : IRequestHandler<Query, List<Company>>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<List<Company>> Handle(Query request, CancellationToken cancellationToken)
            {
                var companies= await _context.Companies.ToListAsync();
                return companies;
            }
        }
    }
}