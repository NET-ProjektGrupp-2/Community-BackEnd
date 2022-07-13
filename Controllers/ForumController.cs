using Community_BackEnd.Data;
using Community_BackEnd.Data.Forums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Community_BackEnd.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ForumController : ControllerBase
{
	private AppDbContext _dbContext;
	public ForumController(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	//Get all Forum Names and Ids
	//ToDo: add counts of topics, ?
	[HttpGet]
	public async Task<IEnumerable<Forum>> Get()
	{
		return await _dbContext.Forums.AsNoTracking().ToListAsync();
	}
	//Get all Topics of Forum with Id = forumId

	[HttpGet("get")]
	public async Task<IActionResult> Get(int forumId)
	{
		var topics = await _dbContext.Forums
			.Include(f => f.Topics)
			.AsNoTracking()
			.Where(f => f.Id == forumId)
			.Take(1)
			.Select(f => f.Topics)
			.FirstOrDefaultAsync();
		return topics == null ? NotFound() : Ok(topics);
	}

	//Get all Posts of Topic with Id = topicId
	[HttpGet("topics/get")]
	public async Task<IActionResult> GetTopic(int topicId)
	{
		var posts = await _dbContext.Topics
			.Include(t => t.Posts)
			.AsNoTracking()
			.Where(t => t.Id == topicId)
			.Take(1)
			.Select(t => t.Posts)
			.FirstOrDefaultAsync();
		return posts == null ? NotFound() : Ok(posts);
	}

	[HttpPost("new")]
	public async Task<IActionResult> NewForumAsync(Forum forum)
	{
		if(ModelState.IsValid)
		{
			_dbContext.Forums.Add(forum);
			return await _dbContext.SaveChangesAsync() == 0 
				? BadRequest() : Ok(forum);
		}
		return BadRequest();
	}

	[HttpPost("topics/new")]
	public async Task<IActionResult> StartTopicAsync(Topic topic)
	{
		if(ModelState.IsValid)
		{
			_dbContext.Topics.Add(topic);
			return await _dbContext.SaveChangesAsync() == 0 
				? BadRequest() : Ok(topic);
		}
		return BadRequest();
	}

	[HttpPost("topics/posts/new")]
	public async Task<IActionResult> PostAsync(Post post)
	{
		if(ModelState.IsValid)
		{
			_dbContext.Posts.Add(post);
			return await _dbContext.SaveChangesAsync() == 0 
				? BadRequest() : Ok(post);
		}
		return BadRequest();
	}

	[HttpPut("move")]
	public async Task<IActionResult> MoveForum(int forumId, int? targetForumId)
	{
		var prop = _dbContext.Attach(new Forum(){Id = forumId}).Property(nameof(Forum.ParentForumId));
		prop.CurrentValue = targetForumId;
		prop.IsModified = true;
		return await _dbContext.SaveChangesAsync() == 0 
			? BadRequest() : Ok();
	}
	[HttpPut("topics/move")]
	public async Task<IActionResult> MoveTopic(int topicId, int forumId)
	{
		Topic movedTopic = new Topic(){Id = topicId};
		_dbContext.Attach(movedTopic);
		movedTopic.ForumId = forumId;
		return await _dbContext.SaveChangesAsync() == 0 
			? BadRequest() : Ok();
	}
	[HttpPut("topics/posts/edit")]
	public async Task<IActionResult> EditPost(int postId, [FromBody] string content)
	{
		DateTime editDate = DateTime.Now;
		if(ModelState.IsValid)
		{
			var postUpdate = new Post(){Id = postId};
			_dbContext.Attach(postUpdate);
			postUpdate.EditDate = editDate;
			postUpdate.Content = content;
			return await _dbContext.SaveChangesAsync() == 0 
				? BadRequest() : Ok(editDate.ToLongDateString());
		}
		return BadRequest();
	}

	[HttpDelete("delete")]
	public async Task<IActionResult> DeleteForum(int id)
	{
		Forum f = new Forum(){ Id = id};
		_dbContext.Attach(f);
		_dbContext.Forums.Remove(f);
		return await _dbContext.SaveChangesAsync() == 0 
			? NotFound() : Ok();
	}
	[HttpDelete("topics/delete")]
	public async Task<IActionResult> DeleteTopic(int id)
	{
		Topic t = new Topic(){ Id = id};
		_dbContext.Attach(t);
		_dbContext.Topics.Remove(t);
		return await _dbContext.SaveChangesAsync() == 0 
			? NotFound() : Ok();
	}
	[HttpDelete("topics/posts/delete")]
	public async Task<IActionResult> DeletePost(int id)
	{
		Post p = new Post(){Id = id};
		_dbContext.Attach(p);
		_dbContext.Posts.Remove(p);
		return await _dbContext.SaveChangesAsync() == 0 
			? NotFound() : Ok();
	}
}
