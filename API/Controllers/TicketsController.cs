using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Tickets;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    public class TicketsController:BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<TicketDto>>> List()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<TicketDto>> Details(Guid id)
        {
            return await Mediator.Send(new Details.Query{Id=id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create([FromForm]Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        [Authorize(Policy="IsTicketHost")]
        public async Task<ActionResult<Unit>> Edit(Guid id,[FromForm]Edit.Command command1) 
        {
            command1.Id=id;
            return await Mediator.Send(command1);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy="IsTicketHost")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new Delete.Command{Id=id});
        }

        [HttpPost("{id}/assign")]
        public async Task<ActionResult<Unit>> Assign (Guid id)
        {
            return await Mediator.Send(new Assign.Command{Id=id});
        }

        [HttpDelete("{id}/assign")]
        public async Task<ActionResult<Unit>> Unassign (Guid id)
        {
            return await Mediator.Send(new Unassign.Command{Id=id});
        }
    }
}