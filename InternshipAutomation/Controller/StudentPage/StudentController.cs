using InternshipAutomation.Controller.Filters;
using InternshipAutomation.Persistance.CQRS.File;
using InternshipAutomation.Persistance.CQRS.Internship;
using InternshipAutomation.Persistance.CQRS.User.StudentUser;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipAutomation.Controller.StudentPage;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer", Roles = IdentityData.AdminAndStudentUserRankName)]
public class StudentController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [AllowAnonymous]
    [HttpPost("RegisterStudent")]
    [ConsoleLog("RegisterStudent - StudentController (no Authenticated)")]
    public async Task<IActionResult> AddStudent([FromQuery] AddStudentCommand addStudentCommand)
    {
        return Ok(await _mediator.Send(addStudentCommand));
    }
    
    [HttpPut("UpdateStudent")]
    [ConsoleLog("UpdateStudent - StudentController")]
    public async Task<IActionResult> UpdateStudent([FromQuery] UpdateStudentCommand updateStudentCommand)
    {
        return Ok(await _mediator.Send(updateStudentCommand));
    }
    
    [HttpPost("RegisterInternship")]
    [ConsoleLog("RegisterInternship - StudentController")]
    public async Task<IActionResult> RegisterInternship([FromQuery] RegisterInternshipCommand registerInternshipCommand)
    {
        return Ok(await _mediator.Send(registerInternshipCommand));
    }
    
    [HttpPut("UpdateInternshipApplication")]
    [ConsoleLog("UpdateInternshipApplication - StudentController")]
    public async Task<IActionResult> UpdateInternshipApplication([FromQuery] UpdateInternshipApplicationCommand updateInternshipApplicationCommand)
    {
        return Ok(await _mediator.Send(updateInternshipApplicationCommand));
    }
    
    [HttpPost("AddDailyReport")]
    [ConsoleLog("AddDailyReport - StudentController")]
    public async Task<IActionResult> AddDailyReportFile([FromQuery] AddDailyReportFileCommand addDailyReportFileCommand)
    {
        return Ok(await _mediator.Send(addDailyReportFileCommand));
    }
    
    [HttpPut("UpdateDailyReport")]
    [ConsoleLog("UpdateDailyReport - StudentController")]
    public async Task<IActionResult> UpdateDailyReportFile([FromQuery] UpdateDailyReportFileCommand updateDailyReportFileCommand)
    {
        return Ok(await _mediator.Send(updateDailyReportFileCommand));
    }
    
    
    [HttpGet("ShowInternshipSituation")]
    [ConsoleLog("ShowInternshipSituation - StudentController")]
    public async Task<IActionResult> ShowInternshipSituation([FromQuery] ShowInternshipSituationCommand showInternshipSituationCommand)
    {
        return Ok(await _mediator.Send(showInternshipSituationCommand));
    }

}