using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternshipAutomation.Persistance.CQRS.Login;
using InternshipAutomation.Persistance.CQRS.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace InternshipAutomation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegisterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromQuery] AddUserCommand addUserCommand)
        {
            return Ok(await _mediator.Send(addUserCommand));
        }
    }
}