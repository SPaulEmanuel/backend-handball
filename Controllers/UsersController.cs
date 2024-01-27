namespace aplicatieHandbal.Controllers;

using Microsoft.AspNetCore.Mvc;
using aplicatieHandbal.Helpers;
using aplicatieHandbal.Models;
using aplicatieHandbal.Services;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(Users model)
    {

        return Ok(await _userService.AddUser(model));
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest model)
    {
        var response = await _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

/*    [Authorize]*/
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAll();
        return Ok(users);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> deleteUser([FromRoute] Guid id)
    {
        return Ok(await _userService.DeleteUser(id));
    }

}
