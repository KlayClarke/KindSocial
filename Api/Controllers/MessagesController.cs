using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
  private readonly MessagesService _messagesService;

  public MessagesController(MessagesService messagesService) => _messagesService = messagesService;

  [HttpGet]
  public async Task<List<Message>> Get() => await _messagesService.GetMessages(); // get request that returns a list of messages

  [HttpGet("{id:length(24)}")]
  public async Task<ActionResult<Message>> Get(string id) // get request that returns specified message (if id matches)
  {
    var message = await _messagesService.GetMessage(id);

    if(message is null)
    {
      return NotFound();
    }

    return message;
  }

  [HttpPost]
  public async Task<IActionResult> Post(Message newMessage) // post request that creates new messsage
  {
    await _messagesService.CreateMessage(newMessage);

    return CreatedAtAction(nameof(Get), new { id = newMessage.Id }, newMessage);
  }

  [HttpPut("{id:length(24)}")]
  public async Task<IActionResult> Update(string id, Message updatedMessage) // put request that updates specified message (if id matches)
  {
    var message = await _messagesService.GetMessage(id);

    if(message is null)
    {
      return NotFound();
    }

    updatedMessage.Id = message.Id;

    await _messagesService.UpdateMessage(id, updatedMessage);

    return NoContent();
  }

  [HttpDelete("{id:length(24)}")]
  public async Task<IActionResult> Delete(string id) // delete request that removes specified message (if id matches)
  {
    var message = await _messagesService.GetMessage(id);

    if(message is null)
    {
      return NotFound();
    }

    await _messagesService.RemoveMessage(id);

    return NoContent();
  }

}