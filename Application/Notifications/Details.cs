using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.Notifications
{
    public class Details
    {
        public class Query : IRequest<Notification>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Notification>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<Notification> Handle(Query request, CancellationToken cancellationToken)
            {
                var notifications = await _context.Notifications.FindAsync(request.Id);
                if (notifications == null)
                    throw new RestException(HttpStatusCode.NotFound, new {notifications = "Could not find" });
                return notifications;
            }
        }
    }
}