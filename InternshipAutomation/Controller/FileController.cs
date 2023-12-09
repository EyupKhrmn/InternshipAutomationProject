using InternshipAutomation.Persistance.CQRS.File;
using InternshipAutomation.Persistance.CQRS.Role;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipAutomation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer",Policy = IdentityData.TeacherUserPolicyName)]
    public class FileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ShowApplicationFileByStudent([FromQuery] GetApplicationFileByStudentNumber getApplicationFileByStudentNumber)
        {
            return Ok(await _mediator.Send(getApplicationFileByStudentNumber));
        }
    }
}