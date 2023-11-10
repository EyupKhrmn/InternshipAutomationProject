using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternshipAutomation.Persistance.CQRS.Internship;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InternshipAutomation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterInternshipPeriodController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegisterInternshipPeriodController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterInternship([FromQuery] RegisterInternshipCommand registerInternshipCommand)
        {
            return Ok(await _mediator.Send(registerInternshipCommand));
        }
    }
}