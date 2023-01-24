using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.Companies
{
    public class Details
    {
        public class Query : IRequest<Company>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Company>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<Company> Handle(Query request, CancellationToken cancellationToken)
            {
                var company = await _context.Companies.FindAsync(request.Id);
                if (company == null)
                    throw new RestException(HttpStatusCode.NotFound, new { company = "Could not find" });
                return company;
            }
        }
    }
}