using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.Role
{
    public class Details
    {
         public class Query : IRequest<AppRole>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, AppRole>
        {
            private readonly ticketContext _context;
            public Handler(ticketContext context)
            {
                _context = context;

            }
            public async Task<AppRole> Handle(Query request, CancellationToken cancellationToken)
            {
                var role = await _context.Roles.FindAsync(request.Id);
                if (role == null)
                    throw new RestException(HttpStatusCode.NotFound, new { role = "Could not find" });
                return role;
            }
        }
    }
}