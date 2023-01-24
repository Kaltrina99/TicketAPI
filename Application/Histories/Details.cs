using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.Histories
{
    public class Details
    {
        public class Query : IRequest<History>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, History>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<History> Handle(Query request, CancellationToken cancellationToken)
            {
                var history = await _context.Histories.FindAsync(request.Id);
                if (history == null)
                    throw new RestException(HttpStatusCode.NotFound, new {history = "Could not find" });
                return history;
            }
        }
    }
}