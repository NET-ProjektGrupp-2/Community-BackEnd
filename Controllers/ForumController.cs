using Community_BackEnd.Data;
using Community_BackEnd.Data.Forums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
			.AsNoTracking() // Optimization when only reading data without any manipulation
			.Where(f => f.Id == forumId)
			.Take(1) // Makes the sql server stop the lookup once one match has been found
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

	// Most of this code is to void making a query to the
	// database if the Context is not tracking the forum.
	[HttpPut("move")]
	public async Task<IActionResult> MoveForum(int forumId, int? targetForumId)
	{
		// If the Context IS tracking the entity, use that because you
		// won't be able to attach another entity with the same Id
		EntityEntry<Forum>? forum = _dbContext.ChangeTracker.Entries<Forum>()
			.FirstOrDefault(x => x.Entity.Id == forumId);

		if(forum == null)
		{
			// If it is not tracked, attaching a dummy object with only the Id and editing only the
			// specific property will make EF send only an Update query to the sql server on save.
			forum = _dbContext.Attach(new Forum() { Id = forumId });
			// When assigning null to a nullable value on a tracked dummy object,
			// the tracker will not notice any change because the value is null by
			// default, so IsModified needs to be set to true manually.
			var prop = forum.Property(f => f.ParentForumId);
			prop.CurrentValue = targetForumId;
			prop.IsModified = true;
			forum.State = EntityState.Modified;
		}
		else
		{
			// If it is tracked, any change to the property will be seen by the tracker
			forum.Entity.ParentForumId = targetForumId;
		}
		return await _dbContext.SaveChangesAsync() == 0 
			? NotFound() : Ok();
	}
	// See MoveForum() for explanation of DbContext tracking
	[HttpPut("topics/move")]
	public async Task<IActionResult> MoveTopic(int topicId, int forumId)
	{
		EntityEntry<Topic>? topic = _dbContext.ChangeTracker.Entries<Topic>()
			.FirstOrDefault(x => x.Entity.Id == topicId);
		if(topic == null)
		{
			topic = _dbContext.Attach(new Topic() { Id = topicId });
		}
		topic.Entity.ForumId = forumId;
		return await _dbContext.SaveChangesAsync() == 0 
			? NotFound() : Ok();
	}
	// See MoveForum() for explanation of DbContext tracking
	[HttpPut("topics/posts/edit")]
	public async Task<IActionResult> EditPost(int postId, string content)
	{
		DateTime editDate = DateTime.Now;
		EntityEntry<Post>? post = _dbContext.ChangeTracker.Entries<Post>()
			.FirstOrDefault(x => x.Entity.Id == postId);
		if(post == null)
		{
			post = _dbContext.Attach(new Post() { Id = postId });
		}
		post.Entity.EditDate = editDate;
		post.Entity.Content = content;
		return await _dbContext.SaveChangesAsync() == 0 
				? NotFound() : Ok(new {editTime = editDate.ToLongDateString() });
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
