using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Comments
{
    public class List
    {
        public class Query : IRequest<List<Comment>> { }

        public class Handler : IRequestHandler<Query, List<Comment>>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<List<Comment>> Handle(Query request, CancellationToken cancellationToken)
            {
                var comments= await _context.Comments.ToListAsync();
                return comments;
            }
        }
    }
}