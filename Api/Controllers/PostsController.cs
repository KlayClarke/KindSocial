using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
  private readonly PostsService _postsService;

  public PostsController(PostsService postsService) => _postsService = postsService;

  [HttpGet]
  public async Task<List<Post>> Get() => await _postsService.GetPosts(); // get request that returns list of posts

  [HttpGet("{id:length(24)}")]
  public async Task<ActionResult<Post>> Get(string id) // get request that returns post (if id matches)
  {
    var post = await _postsService.GetPost(id);

    if(post is null)
    {
      return NotFound();
    }

    return post;
  }

  [HttpPost]
  public async Task<IActionResult> Post(Post newPost) // post request that creates new post
  {
    await _postsService.CreatePost(newPost);

    return CreatedAtAction(nameof(Get), new {id = newPost.Id}, newPost);
  }

  [HttpPut("{id:length(24)}")]
  public async Task<IActionResult> Update(string id, Post updatedPost) // put request that updates post (if id matches)
  {
    var post = await _postsService.GetPost(id);

    if(post is null)
    {
      return NotFound();
    }

    updatedPost.Id = post.Id;

    await _postsService.UpdatePost(id, updatedPost);

    return NoContent();
  }

  [HttpDelete("{id:length(24)}")]
  public async Task<IActionResult> Delete(string id) // delete request that removes post (if id matches)
  {
    var post = await _postsService.GetPost(id);

    if(post is null)
    {
      return NotFound();
    }

    await _postsService.RemovePost(id);

    return NoContent();
  }
}