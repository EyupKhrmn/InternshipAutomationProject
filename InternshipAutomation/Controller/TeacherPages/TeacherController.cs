using InternshipAutomation.Persistance.CQRS.File;
using InternshipAutomation.Persistance.CQRS.Internship;
using InternshipAutomation.Persistance.CQRS.User.TeacherUser;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipAutomation.Controller.TeacherPages;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer", Policy = IdentityData.TeacherUserPolicyName)]
public class TeacherController : ControllerBase
{
    private readonly IMediator _mediator;

    public TeacherController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("RegisterTeacher")]
    public async Task<IActionResult> AddTeacher([FromQuery] AddTeacherCommand addTeacherCommand)
    {
        return Ok(await _mediator.Send(addTeacherCommand));
    }

    [HttpPut("UpdateTeacher")]
    public async Task<IActionResult> UpdateTeacher([FromQuery] UpdateTeacherCommand updateTeacherCommand)
    {
        return Ok(await _mediator.Send(updateTeacherCommand));
    }

    [HttpPost("GiveNoteForInternship")]
    public async Task<IActionResult> GiveNoteForInternship(
        [FromQuery] GiveNoteForInternshipCommand giveNoteForInternshipCommand)
    {
        return Ok(await _mediator.Send(giveNoteForInternshipCommand));
    }

    [HttpPost("StartInternshipPeriod")]
    public async Task<IActionResult> StartInternshipPeriod([FromQuery] InternshipPeriodCommand ınternshipPeriodCommand)
    {
        return Ok(await _mediator.Send(ınternshipPeriodCommand));
    }

    [HttpGet("GetApplicationFileByStudentNumber")]
    public async Task<IActionResult> ShowApplicationFileByStudent(
        [FromQuery] GetApplicationFileByStudentNumber getApplicationFileByStudentNumber)
    {
        return Ok(await _mediator.Send(getApplicationFileByStudentNumber));
    }

    [HttpGet("GetStudentDailyReports")]
    public async Task<IActionResult> GetStudentDailyReports(
        [FromQuery] GetDailyReportFileCommand getDailyReportFileCommand)
    {
        return Ok(await _mediator.Send(getDailyReportFileCommand));
    }
    
    [HttpGet("GetAllDailyReport")]
    public async Task<IActionResult> GetAllDailyReport(
        [FromQuery] GetAllDailyReportCommand getAllDailyReportCommand)
    {
        return Ok(await _mediator.Send(getAllDailyReportCommand));
    }

    [HttpPost("GiveNoteForDailyReport")]
    public async Task<IActionResult> GiveNoteForDailyReport(
        [FromQuery] GiveNoteForDailyReportFileCommand giveNoteForDailyReportFileCommand)
    {
        return Ok(await _mediator.Send(giveNoteForDailyReportFileCommand));
    }

    [HttpGet("GetAllInternships")]
    public async Task<IActionResult> GetAllInternships([FromQuery] GetAllInternshipCommand getAllInternshipCommand)
    {
        return Ok(await _mediator.Send(getAllInternshipCommand));
    }
    
    [HttpGet("GetInternship")]
    public async Task<IActionResult> GetInternship([FromQuery] GetInternshipCommand getInternshipCommand)
    {
        return Ok(await _mediator.Send(getInternshipCommand));
    }

    [HttpGet("GetInternshipResultReport")]
    public async Task<IActionResult> GetInternshipResultReport([FromQuery] GetInternshipResultReportCommand getInternshipResultReportCommand)
    {
        return Ok(await _mediator.Send(getInternshipResultReportCommand));
    }
    
    [HttpPut("ChangeStatusForInternship")]
    public async Task<IActionResult> ChangeStatusForInternship([FromQuery] ChangeStatusForInternshipCommand changeStatusForInternshipCommand)
    {
        return Ok(await _mediator.Send(changeStatusForInternshipCommand));
    }
}