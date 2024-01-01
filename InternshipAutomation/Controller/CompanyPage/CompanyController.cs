using InternshipAutomation.Persistance.CQRS.File;
using InternshipAutomation.Persistance.CQRS.Internship;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipAutomation.Controller.CompanyPage;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer", Policy = IdentityData.CompanyUserPolicyName)]
public class CompanyController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompanyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("EvaluateStudent")]
    public async Task<IActionResult> EvaluateStudent([FromQuery] EvaluateStudentCommand evaluateStudentCommand)
    {
        return Ok(await _mediator.Send(evaluateStudentCommand));
    }

    [HttpGet("GetAllIntern")]
    public async Task<IActionResult> GetAllIntern([FromQuery] GetAllInternCommand getAllInternCommand)
    {
        return Ok(await _mediator.Send(getAllInternCommand));
    }
}