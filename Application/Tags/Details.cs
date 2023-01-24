using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.Tags
{
    public class Details
    {
        public class Query : IRequest<Tag>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Tag>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<Tag> Handle(Query request, CancellationToken cancellationToken)
            {
                var tag = await _context.Tags.FindAsync(request.Id);
                if (tag == null)
                    throw new RestException(HttpStatusCode.NotFound, new {tag = "Could not find" });
                return tag;
            }
        }
    }
}