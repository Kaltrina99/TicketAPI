using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Role
{
    public class List
    {
        public class Query : IRequest<List<AppRole>> { }

        public class Handler : IRequestHandler<Query, List<AppRole>>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<List<AppRole>> Handle(Query request, CancellationToken cancellationToken)
            {
                var role= await _context.Roles.ToListAsync();
                return role;
            }
        }
    }
}