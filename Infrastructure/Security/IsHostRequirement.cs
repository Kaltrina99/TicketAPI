using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Infrastructure.Security
{
    public class IsHostRequirement : IAuthorizationRequirement
    {

    }

    public class IsHostRequirementHandler : AuthorizationHandler<IsHostRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ticketContext _context;
        public IsHostRequirementHandler(IHttpContextAccessor httpContextAccessor, ticketContext context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsHostRequirement requirement)
        {
            var currentUserName=_httpContextAccessor.HttpContext.User?.Claims?.SingleOrDefault(x=>x.Type==ClaimTypes.NameIdentifier)?.Value;

            var ticketId=Guid.Parse(_httpContextAccessor.HttpContext.Request.RouteValues.SingleOrDefault(x=>x.Key=="id").Value.ToString());

            var ticket=_context.Tickets.FindAsync(ticketId).Result;
            var host=ticket.UserTickets.FirstOrDefault(x=>x.IsHost);

            if(host?.User?.UserName==currentUserName)
                context.Succeed(requirement);


            return Task.CompletedTask;
        }
    }
}