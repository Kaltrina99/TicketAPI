using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Reports
{
    public class List
    {
        public class Query : IRequest<List<Report>> { }

        public class Handler : IRequestHandler<Query, List<Report>>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<List<Report>> Handle(Query request, CancellationToken cancellationToken)
            {
                var reports= await _context.Reports.ToListAsync();
                return reports;
            }
        }
    }
}