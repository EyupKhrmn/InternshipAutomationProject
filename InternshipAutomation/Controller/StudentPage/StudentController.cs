using InternshipAutomation.Persistance.CQRS.File;
using InternshipAutomation.Persistance.CQRS.Internship;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipAutomation.Controller.StudentPage;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer", Policy = IdentityData.StudentUserPolicyName)]
public class StudentController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("RegisterInternship")]
    public async Task<IActionResult> RegisterInternship([FromQuery] RegisterInternshipCommand registerInternshipCommand)
    {
        return Ok(await _mediator.Send(registerInternshipCommand));
    }
    
    [HttpPost("AddDailyReport")]
    public async Task<IActionResult> AddDailyReportFile([FromQuery] AddDailyReportFileCommand addDailyReportFileCommand)
    {
        if (!ModelState.IsValid)
        {
            return Ok(StatusCode(6161));
        }
        return Ok(await _mediator.Send(addDailyReportFileCommand));
    }

}