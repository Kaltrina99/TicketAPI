using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.Comments
{
    public class Details
    {
        public class Query : IRequest<Comment>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Comment>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<Comment> Handle(Query request, CancellationToken cancellationToken)
            {
                var comment = await _context.Comments.FindAsync(request.Id);
                if (comment == null)
                    throw new RestException(HttpStatusCode.NotFound, new {comment = "Could not find" });
                return comment;
            }
        }
    }
}