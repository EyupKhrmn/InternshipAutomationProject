using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using InternshipAutomation.Persistance.CQRS.Role;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace InternshipAutomation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer",Policy = IdentityData.TeacherUserPolicyName)]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetRolesByFilter([FromQuery] GetRoleCommand getRoleCommand)
        {
            return Ok(await _mediator.Send(getRoleCommand));
        }


        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] AddRoleCommand addRoleCommand)
        {
            return Ok(await _mediator.Send(addRoleCommand));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleCommand updateRoleCommand)
        {
            return Ok(await _mediator.Send(updateRoleCommand));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRole([FromBody] DeleteRoleCommand deleteRoleCommand)
        {
            return Ok(await _mediator.Send(deleteRoleCommand));
        }
    }
}
