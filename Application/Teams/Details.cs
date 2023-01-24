using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.Teams
{
    public class Details
    {
        public class Query : IRequest<Team>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Team>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<Team> Handle(Query request, CancellationToken cancellationToken)
            {
                var teams = await _context.Teams.FindAsync(request.Id);
                if (teams == null)
                    throw new RestException(HttpStatusCode.NotFound, new {teams = "Could not find" });
                return teams;
            }
        }
    }
}