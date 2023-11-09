 using System;
using System.Collections.Generic;
 using System.ComponentModel;
 using System.Linq;
using System.Threading.Tasks;
 using InternshipAutomation.Persistance.CQRS.User;
 using MediatR;
 using Microsoft.AspNetCore.Authentication.JwtBearer;
 using Microsoft.AspNetCore.Authorization;
 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternshipAutomation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetUserByFilter")]
        public async Task<IActionResult> GetUser([FromQuery] GetUserCommand getUserCommand)
        {
            return Ok(await _mediator.Send(getUserCommand));
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUserCommand addUserCommand)
        {
            return Ok(await _mediator.Send(addUserCommand));
        }

        [Authorize("Yönetici")] 
        [HttpPost("AddClaimForuser")]
        public async Task<IActionResult> AddClaim([FromBody] AddClaimCommand addClaimCommand)
        {
            return Ok(await _mediator.Send(addClaimCommand));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand updateUserCommand)
        {
            return Ok(await _mediator.Send(updateUserCommand));
        }

        [HttpPut("AddUserRole")]
        public async Task<IActionResult> AddUserRole([FromQuery] AddUserRoleCommand addUserRoleCommand)
        {
            return Ok(await _mediator.Send(addUserRoleCommand));
        }
    }
}
