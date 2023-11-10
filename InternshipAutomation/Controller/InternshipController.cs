using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternshipAutomation.Persistance.CQRS.Internship;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternshipAutomation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternshipController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InternshipController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> AddInternship([FromQuery] AddInternshipCommand addInternshipCommand)
        {
            return Ok(await _mediator.Send(addInternshipCommand));
        }
    }
}
