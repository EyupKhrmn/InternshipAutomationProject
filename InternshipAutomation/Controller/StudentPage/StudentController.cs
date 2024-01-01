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
    
    [HttpPut("UpdateInternshipApplication")]
    public async Task<IActionResult> UpdateInternshipApplication([FromQuery] UpdateInternshipApplicationCommand updateInternshipApplicationCommand)
    {
        return Ok(await _mediator.Send(updateInternshipApplicationCommand));
    }
    
    [HttpPost("AddDailyReport")]
    public async Task<IActionResult> AddDailyReportFile([FromQuery] AddDailyReportFileCommand addDailyReportFileCommand)
    {
        return Ok(await _mediator.Send(addDailyReportFileCommand));
    }
    
    [HttpPut("UpdateDailyReport")]
    public async Task<IActionResult> UpdateDailyReportFile([FromQuery] UpdateDailyReportFileCommand updateDailyReportFileCommand)
    {
        return Ok(await _mediator.Send(updateDailyReportFileCommand));
    }
    
    
    [HttpGet("ShowInternshipSituation")]
    public async Task<IActionResult> ShowInternshipSituation([FromQuery] ShowInternshipSituationCommand showInternshipSituationCommand)
    {
        return Ok(await _mediator.Send(showInternshipSituationCommand));
    }

}