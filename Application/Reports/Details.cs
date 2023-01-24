using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.Reports
{
    public class Details
    {
        public class Query : IRequest<Report>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Report>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<Report> Handle(Query request, CancellationToken cancellationToken)
            {
                var report = await _context.Reports.FindAsync(request.Id);
                if (report == null)
                    throw new RestException(HttpStatusCode.NotFound, new {report = "Could not find" });
                return report;
            }
        }
    }
}