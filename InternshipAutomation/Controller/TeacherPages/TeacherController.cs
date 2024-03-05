using InternshipAutomation.Controller.Filters;
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
[Authorize(AuthenticationSchemes = "Bearer", Roles = IdentityData.AdminAndTeacherUserRankName)]
public class TeacherController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IDecodeTokenService _decodeTokenService;
    
    

    public TeacherController(IMediator mediator, IDecodeTokenService decodeTokenService)
    {
        _mediator = mediator;
        _decodeTokenService = decodeTokenService;
    }

    [HttpPut("UpdateTeacher")]
    [ConsoleLog("UpdateTeacher - TeacherController")]
    public async Task<IActionResult> UpdateTeacher([FromQuery] UpdateTeacherCommand updateTeacherCommand)
    {
        return Ok(await _mediator.Send(updateTeacherCommand));
    }

    [HttpPost("GiveNoteForInternship")]
    [ConsoleLog("GiveNoteForInternship - TeacherController")]
    public async Task<IActionResult> GiveNoteForInternship(
        [FromQuery] GiveNoteForInternshipCommand giveNoteForInternshipCommand)
    {
        return Ok(await _mediator.Send(giveNoteForInternshipCommand));
    }

    [HttpPost("StartInternshipPeriod")]
    [ConsoleLog("StartInternshipPeriod - TeacherController")]
    public async Task<IActionResult> StartInternshipPeriod([FromQuery] InternshipPeriodCommand internshipPeriodCommand)
    {
        return Ok(await _mediator.Send(internshipPeriodCommand));
    }

    [HttpGet("GetApplicationFileByStudentNumber")]
    [ConsoleLog("GetApplicationFileByStudentNumber - TeacherController")]
    public async Task<IActionResult> ShowApplicationFileByStudent(
        [FromQuery] GetApplicationFileByStudentNumber getApplicationFileByStudentNumber)
    {
        return Ok(await _mediator.Send(getApplicationFileByStudentNumber));
    }

    [HttpGet("GetStudentDailyReports")]
    [ConsoleLog("GetStudentDailyReports - TeacherController")]
    public async Task<IActionResult> GetStudentDailyReports(
        [FromQuery] GetDailyReportFileCommand getDailyReportFileCommand)
    {
        return Ok(await _mediator.Send(getDailyReportFileCommand));
    }
    
    [HttpGet("GetAllDailyReport")]
    [ConsoleLog("GetAllDailyReport - TeacherController")]
    public async Task<IActionResult> GetAllDailyReport(
        [FromQuery] GetAllDailyReportCommand getAllDailyReportCommand)
    {
        return Ok(await _mediator.Send(getAllDailyReportCommand));
    }

    [HttpPost("GiveNoteForDailyReport")]
    [ConsoleLog("GiveNoteForDailyReport - TeacherController")]
    public async Task<IActionResult> GiveNoteForDailyReport(
        [FromQuery] GiveNoteForDailyReportFileCommand giveNoteForDailyReportFileCommand)
    {
        return Ok(await _mediator.Send(giveNoteForDailyReportFileCommand));
    }

    [HttpGet("GetAllInternships")]
    [ConsoleLog("GetAllInternships - TeacherController")]
    public async Task<IActionResult> GetAllInternships([FromQuery] GetAllInternshipCommand getAllInternshipCommand)
    {
        return Ok(await _mediator.Send(getAllInternshipCommand));
    }
    
    [HttpGet("GetInternship")]
    [ConsoleLog("GetInternship - TeacherController")]
    public async Task<IActionResult> GetInternship([FromQuery] GetInternshipCommand getInternshipCommand)
    {
        return Ok(await _mediator.Send(getInternshipCommand));
    }

    [HttpGet("GetInternshipResultReport")]
    [ConsoleLog("GetInternshipResultReport - TeacherController")]
    public async Task<IActionResult> GetInternshipResultReport([FromQuery] GetInternshipResultReportCommand getInternshipResultReportCommand)
    {
        return Ok(await _mediator.Send(getInternshipResultReportCommand));
    }
    
    [HttpPut("ChangeStatusForInternship")]
    [ConsoleLog("ChangeStatusForInternship - TeacherController")]
    public async Task<IActionResult> ChangeStatusForInternship([FromQuery] ChangeStatusForInternshipCommand changeStatusForInternshipCommand)
    {
        return Ok(await _mediator.Send(changeStatusForInternshipCommand));
    }
}