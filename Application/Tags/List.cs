using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Tags
{
    public class List
    {
        public class Query : IRequest<List<Tag>> { }

        public class Handler : IRequestHandler<Query, List<Tag>>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<List<Tag>> Handle(Query request, CancellationToken cancellationToken)
            {
                var tags= await _context.Tags.ToListAsync();
                return tags;
            }
        }
    }
}