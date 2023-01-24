using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Notifications
{
    public class List
    {
        public class Query : IRequest<List<Notification>> { }

        public class Handler : IRequestHandler<Query, List<Notification>>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<List<Notification>> Handle(Query request, CancellationToken cancellationToken)
            {
                var notifications= await _context.Notifications.ToListAsync();
                return notifications;
            }
        }
    }
}