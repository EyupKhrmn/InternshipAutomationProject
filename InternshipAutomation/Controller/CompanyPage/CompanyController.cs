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

}