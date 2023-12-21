using InternshipAutomation.Persistance.CQRS.File;
using InternshipAutomation.Persistance.CQRS.Internship;
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
    
    [HttpPost("GiveNoteForInternship")]
    public async Task<IActionResult> GiveNoteForInternship([FromQuery] GiveNoteForInternshipCommand giveNoteForInternshipCommand)
    {
        return Ok(await _mediator.Send(giveNoteForInternshipCommand));
    }

    [HttpPost("StartInternshipPeriod")]
    public async Task<IActionResult> StartInternshipPeriod([FromQuery] InternshipPeriodCommand ınternshipPeriodCommand)
    {
        return Ok(await _mediator.Send(ınternshipPeriodCommand));
    }
    
    [HttpGet("GetApplicationFileByStudentNumber")]
    public async Task<IActionResult> ShowApplicationFileByStudent([FromQuery] GetApplicationFileByStudentNumber getApplicationFileByStudentNumber)
    {
        return Ok(await _mediator.Send(getApplicationFileByStudentNumber));
    }
}
