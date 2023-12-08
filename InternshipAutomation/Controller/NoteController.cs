using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternshipAutomation.Persistance.CQRS.Internship;
using InternshipAutomation.Persistance.CQRS.Login;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InternshipAutomation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer",Policy = IdentityData.TeacherUserPolicyName)]
    public class NoteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> GiveNoteForInternship([FromQuery] GiveNoteForInternshipCommand giveNoteForInternshipCommand)
        {
            return Ok(await _mediator.Send(giveNoteForInternshipCommand));
        }
    }
}