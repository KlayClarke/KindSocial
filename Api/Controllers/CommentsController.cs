using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
  private readonly CommentsService _commentsService;

  public CommentsController(CommentsService commentsService) => _commentsService = commentsService;

  [HttpGet]
  public async Task<List<Comment>> Get() => await _commentsService.GetComments(); // get request that returns list of comments

  [HttpGet("{id:length(24)}")]
  public async Task<ActionResult<Comment>> Get(string id) // get request that returns specified comment (if id matches)
  {
    var comment = await _commentsService.GetComment(id);

    if (comment is null)
    {
      return NotFound();
    }

    return comment;
  }

  [HttpPost]
  public async Task<IActionResult> Post(Comment newComment) // post request that creates new comment
  {
    await _commentsService.CreateComment(newComment);

    return CreatedAtAction(nameof(Get), new { id = newComment.Id }, newComment);
  }

  [HttpPut("{id:length(24)}")]
  public async Task<IActionResult> Update(string id, Comment updatedComment) // put request that updates specified comment (if id matches)
  {
    var comment = await _commentsService.GetComment(id);

    if (comment is null)
    {
      return NotFound();
    }

    updatedComment.Id = comment.Id;

    await _commentsService.UpdateComment(id, updatedComment);

    return NoContent();
  }

  [HttpDelete("{id:length(24)}")]
  public async Task<IActionResult> Delete(string id) // delete request that removes specified comment (if id matches)
  {
    var comment = await _commentsService.GetComment(id);

    if (comment is null)
    {
      return NotFound();
    }

    await _commentsService.RemoveComment(id);

    return NoContent();
  }
}