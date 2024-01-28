namespace aplicatieHandbal.Controllers;

using Microsoft.AspNetCore.Mvc;
using aplicatieHandbal.Helpers;
using aplicatieHandbal.Models;
using aplicatieHandbal.Services;
using CSU_Suceava_BE.Application.JwtUtils;
using Microsoft.AspNetCore.Authorization;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [AuthorizeMultiplePolicy(Policies.Administrator, true)]
    [HttpPost]
    public async Task<IActionResult> AddUser(Users model)
    {

        return Ok(await _userService.AddUser(model));
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest model)
    {
        var response = await _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

    [AuthorizeMultiplePolicy(Policies.Administrator, true)]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAll();
        return Ok(users);
    }

    [AuthorizeMultiplePolicy(Policies.Administrator, true)]
    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> updateUser([FromRoute] Guid id, Users updateUserReq)
    {
        return Ok(await _userService.UpdateUser(id, updateUserReq));
    }

    [AuthorizeMultiplePolicy(Policies.Administrator, true)]
    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> deleteUser([FromRoute] Guid id)
    {
        return Ok(await _userService.DeleteUser(id));
    }

}
