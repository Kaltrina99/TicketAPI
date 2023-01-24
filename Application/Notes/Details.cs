using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.Notes
{
    public class Details
    {
        public class Query : IRequest<Note>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Note>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<Note> Handle(Query request, CancellationToken cancellationToken)
            {
                var note = await _context.Notes.FindAsync(request.Id);
                if (note == null)
                    throw new RestException(HttpStatusCode.NotFound, new {note = "Could not find" });
                return note;
            }
        }
    }
}