using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.Attachments
{
    public class Details
    {
        public class Query : IRequest<Attachment>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Attachment>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<Attachment> Handle(Query request, CancellationToken cancellationToken)
            {
                var attachment = await _context.Attachments.FindAsync(request.Id);
                if (attachment == null)
                    throw new RestException(HttpStatusCode.NotFound, new {attachment = "Could not find" });
                return attachment;
            }
        }
    }
}