using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Role;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    public class RoleController:BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<AppRole>>> List()
        {
            return await Mediator.Send(new List.Query());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AppRole>> Details(string id)
        {
            return await Mediator.Send(new Details.Query{Id=id});
        }
        
        [HttpPost]
        public async Task<ActionResult<Unit>> Create([FromForm]Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(string id,[FromForm]Edit.Command command1) 
        {
            command1.Id=id;
            return await Mediator.Send(command1);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(string id)
        {
            return await Mediator.Send(new Delete.Command{Id=id});
        }
    }
}