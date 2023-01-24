using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using MediatR;
using Persistence;

namespace Application.TicketPriorities
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var ticketPriority = await _context.TicketPriorities.FindAsync(request.Id);
                if (ticketPriority == null)
                    throw new RestException(HttpStatusCode.NotFound, new { ticketPriority = "Could not find" });

                _context.Remove(ticketPriority);

                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;
                throw new Exception("Problem saving the data");
            }
        }
    }
}