using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Departments
{
    public class List
    {
        public class Query : IRequest<List<Department>> { }

        public class Handler : IRequestHandler<Query, List<Department>>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<List<Department>> Handle(Query request, CancellationToken cancellationToken)
            {
                var departments= await _context.Departments.ToListAsync();
                return departments;
            }
        }
    }
}