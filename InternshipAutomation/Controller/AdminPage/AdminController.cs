using InternshipAutomation.Persistance.CQRS.Role;
using InternshipAutomation.Persistance.CQRS.User;
using InternshipAutomation.Persistance.CQRS.User.AdminUser;
using InternshipAutomation.Security.Token;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipAutomation.Controller.AdminPage;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer", Roles = IdentityData.AdminUserRankName)]
public class AdminController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    
    [HttpGet("GetUserByFilter")]
    public async Task<IActionResult> GetUser([FromQuery] GetUserCommand getUserCommand)
    {
        return Ok(await _mediator.Send(getUserCommand));
    }
        
    [HttpPost("AddUser")]
    public async Task<IActionResult> AddUser([FromQuery] AddUserCommand addUserCommand)
    {
        return Ok(await _mediator.Send(addUserCommand));
    }
    
    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser([FromQuery] UpdateUserCommand updateUserCommand)
    {
        return Ok(await _mediator.Send(updateUserCommand));
    }
    
    [HttpDelete("DeleteUser")]
    public async Task<IActionResult> DeleteUser([FromQuery] DeleteUserCommand deleteUserCommand)
    {
        return Ok(await _mediator.Send(deleteUserCommand));
    }

    [HttpPut("AddUserRole")]
    public async Task<IActionResult> AddUserRole([FromQuery] AddUserRoleCommand addUserRoleCommand)
    {
        return Ok(await _mediator.Send(addUserRoleCommand));
    }
    
    [HttpGet("GetRoleByFilter")]
    public async Task<IActionResult> GetRolesByFilter([FromQuery] GetRoleCommand getRoleCommand)
    {
        return Ok(await _mediator.Send(getRoleCommand));
    }


    [HttpPost("AddRole")]
    public async Task<IActionResult> AddRole([FromBody] AddRoleCommand addRoleCommand)
    {
        return Ok(await _mediator.Send(addRoleCommand));
    }

    [HttpPut("UpdateRole")]
    public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleCommand updateRoleCommand)
    {
        return Ok(await _mediator.Send(updateRoleCommand));
    }

    [HttpDelete("DeleteRole")]
    public async Task<IActionResult> DeleteRole([FromBody] DeleteRoleCommand deleteRoleCommand)
    {
        return Ok(await _mediator.Send(deleteRoleCommand));
    }
    
    [HttpPost("AddClaimForUser")]
    public async Task<IActionResult> AddClaim([FromQuery] AddClaimCommand addClaimCommand)
    {
        return Ok(await _mediator.Send(addClaimCommand));
    }
}