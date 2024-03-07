using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternshipAutomation.Controller.Filters;
using InternshipAutomation.Persistance.CQRS.Login;
using InternshipAutomation.Persistance.CQRS.TimeoutData;
using InternshipAutomation.Persistance.CQRS.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace InternshipAutomation.Controller
{
    [RequestTimeout(TimeoutMessage.MoreThanOneMinute)]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Login")]
        [ConsoleLog("Login - LoginController")]
        public async Task<IActionResult> Login([FromQuery] LoginCommand loginCommand,CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(loginCommand,cancellationToken));
        }

        [HttpPut("ForgotPassword")]
        [ConsoleLog("ForgotPassword - LoginController")]
        public async Task<IActionResult> ForgotPassword([FromQuery] ForgotPasswordCommand forgotPasswordCommand)
        {
            return Ok(await _mediator.Send(forgotPasswordCommand));
        }
        
        [HttpPut("ResetPassword")]
        [ConsoleLog("ResetPassword - LoginController")]
        public async Task<IActionResult> ResetPassword([FromQuery] ResetPasswordCommand resetPasswordCommand)
        {
            return Ok(await _mediator.Send(resetPasswordCommand));
        }
    }
}
