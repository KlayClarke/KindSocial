using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
  private readonly UsersService _usersService;

  public UsersController(UsersService usersService) => _usersService = usersService;

  [HttpGet]
  public async Task<List<User>> Get() => await _usersService.GetUsers(); // get request that returns list of users

  [HttpGet("{id:length(24)}")]
  public async Task<ActionResult<User>> Get(string id) // get request that returns user (if id matches)
  {
    var user = await _usersService.GetUser(id);

    if(user is null)
    {
      return NotFound();
    }

    return user;
  }

  [HttpPost]
  public async Task<IActionResult> Post(User newUser) // post request that creates new user
  {
    await _usersService.CreateUser(newUser);

    return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
  }

  [HttpPut("{id:length(24)}")]
  public async Task<IActionResult> Update(string id, User updatedUser) // put request that updates user (if id matches)
  {
    var user = await _usersService.GetUser(id);

    if(user is null)
    {
      return NotFound();
    }

    updatedUser.Id = user.Id;

    await _usersService.UpdateUser(id, updatedUser);

    return NoContent();
  }

  [HttpDelete("{id:length(24)}")]
  public async Task<IActionResult> Delete(string id) // delete request that removes user (if id matches)
  {
    var user = await _usersService.GetUser(id);

    if(user is null)
    {
      return NotFound();
    }

    await _usersService.RemoveUser(id);

    return NoContent();

  }
}