using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.Departments
{
    public class Details
    {
        public class Query : IRequest<Department>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Department>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<Department> Handle(Query request, CancellationToken cancellationToken)
            {
                var department = await _context.Departments.FindAsync(request.Id);
                if (department == null)
                    throw new RestException(HttpStatusCode.NotFound, new {department = "Could not find" });
                return department;
            }
        }
    }
}