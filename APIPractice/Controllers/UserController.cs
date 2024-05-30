using Microsoft.AspNetCore.Mvc;
using Practice.Entities;

namespace Practice.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService iUS)
    {
        userService = iUS;
    }

    [HttpPost]
    public async Task<ActionResult<User>> PostNewUser(User newUser)
    {
        try
        {
            await userService.CreateNewUserAsync(newUser);

            return Ok(newUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}