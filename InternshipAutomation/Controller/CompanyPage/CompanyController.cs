using InternshipAutomation.Controller.Filters;
using InternshipAutomation.Persistance.CQRS.File;
using InternshipAutomation.Persistance.CQRS.Internship;
using InternshipAutomation.Persistance.CQRS.TimeoutData;
using InternshipAutomation.Persistance.CQRS.User.CompanyUser;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;

namespace InternshipAutomation.Controller.CompanyPage;

[RequestTimeout(TimeoutMessage.MoreThanOneMinute)]
[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer", Roles = IdentityData.AdminAndCompanyUserRankName)]
public class CompanyController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompanyController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPut("UpdateCompanyUser")]
    [ConsoleLog("UpdateCompanyUser - CompanyController")]
    public async Task<IActionResult> UpdateCompanyUser([FromQuery] UpdateCompanyUserCommand updateCompanyUserCommand)
    {
        return Ok(await _mediator.Send(updateCompanyUserCommand));
    }

    [HttpPost("EvaluateStudent")]
    [ConsoleLog("EvaluateStudent - CompanyController")]
    public async Task<IActionResult> EvaluateStudent([FromQuery] EvaluateStudentCommand evaluateStudentCommand)
    {
        return Ok(await _mediator.Send(evaluateStudentCommand));
    }

    [HttpGet("GetAllIntern")]
    [ConsoleLog("GetAllIntern - CompanyController")]
    public async Task<IActionResult> GetAllIntern([FromQuery] GetAllInternCommand getAllInternCommand)
    {
        return Ok(await _mediator.Send(getAllInternCommand));
    }
    
    [HttpGet("GetAllInternshipDailyReportForIntern")]
    [ConsoleLog("GetAllInternshipDailyReportForIntern - CompanyController")]
    public async Task<IActionResult> GetAllInternshipDailyReportForIntern([FromQuery] GetAllInternshipDailyReportForInternCommand getAllInternshipDailyReportForInternCommand)
    {
        return Ok(await _mediator.Send(getAllInternshipDailyReportForInternCommand));
    }
    
    [HttpPut("CheckInternshipDailyReport")]
    [ConsoleLog("CheckInternshipDailyReport - CompanyController")]
    public async Task<IActionResult> CheckInternshipDailyReport([FromQuery] CheckInternshipDailyReportCommand checkInternshipDailyReportCommand)
    {
        return Ok(await _mediator.Send(checkInternshipDailyReportCommand));
    }
}