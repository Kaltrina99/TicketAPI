using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.TicketStatuses;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    public class TicketStatusesController:BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<TicketStatus>>> List()
        {
            return await Mediator.Send(new List.Query());
        }   

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketStatus>> Details(Guid id)
        {
            return await Mediator.Send(new Details.Query{Id=id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create([FromForm]Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id,[FromForm]Edit.Command command) 
        {
            command.Id=id;
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new Delete.Command{Id=id});
        }
    }
}