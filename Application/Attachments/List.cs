using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Attachments
{
    public class List
    {
        public class Query : IRequest<List<Attachment>> { }

        public class Handler : IRequestHandler<Query, List<Attachment>>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<List<Attachment>> Handle(Query request, CancellationToken cancellationToken)
            {
                var attachments= await _context.Attachments.ToListAsync();
                return attachments;
            }
        }
    }
}