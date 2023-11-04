using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternshipAutomation.Persistance.CQRS.Files;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace InternshipAutomation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddFileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddFileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddFiles([FromQuery] AddFileCommand addFileCommand)
        {
            return Ok(await _mediator.Send(addFileCommand));
        }
    }
}
