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
	//Get all Topics of Forum with Id = id

	[HttpGet("{forumId}")]
	public async Task<IEnumerable<Topic>> Get(int forumId)
	{
		return (await _dbContext.Forums
			.Include(f => f.Topics)
			.AsNoTracking()
			.FirstAsync(f => f.Id == forumId))
				.Topics.ToList();
	}
	//Get all Posts of Topic with Id = id

	[HttpGet("{topicId}")]
	public async Task<IEnumerable<Post>> GetTopic(int topicId)
	{
		 return await _dbContext.Topics.Include(t => t.Posts).AsNoTracking().Where(t => t.Id == topicId).Select((t, i) => t.Posts).FirstAsync();
	}

	[HttpPost("forum")]
	public IActionResult NewForum(Forum forum)
	{
		if(ModelState.IsValid)
		{
			_dbContext.Forums.Add(forum);
			_dbContext.SaveChangesAsync();
			return Ok();
		}
		return BadRequest();
	}

	[HttpPost("topic")]
	public IActionResult StartTopic(Topic topic)
	{
		if(ModelState.IsValid)
		{
			_dbContext.Topics.Add(topic);
			_dbContext.SaveChangesAsync();
			return Ok();
		}
		return BadRequest();
	}

	[HttpPost("post")]
	public IActionResult Post(Post post)
	{
		if(ModelState.IsValid)
		{
			_dbContext.Posts.Add(post);
			_dbContext.SaveChanges();
			return Ok(new {PostId = post.Id});
		}
		return BadRequest();
	}

	[HttpPut("{forumId,targetForumId}")]
	public async Task<IActionResult> MoveForum(int forumId, int targetForumId)
	{
		var movedForum = new Forum(){Id = forumId};
		_dbContext.Attach(movedForum);
		movedForum.ParentForumId = targetForumId;
		return await _dbContext.SaveChangesAsync() == 0 ? BadRequest() : Ok();
		//Forum? movedForum = await _dbContext.Forums.FindAsync(forumId);
		//if(movedForum != null)
		//{
		//	if(targetForumId == 0)
		//	{
		//		movedForum.ParentForumId = null;
		//		return Ok();
		//	}
		//	else
		//	{
		//		if(true)
		//		{

		//		}
		//		Forum? targetForum = await _dbContext.Forums.FindAsync(targetForumId);
		//		if(targetForum != null)
		//		{
		//			if(movedForum.ParentForumId != null)
		//			{
		//				movedForum.ParentForum.SubForums.Remove(movedForum);
		//			}
		//			movedForum.ParentForum = targetForum;
		//			targetForum.SubForums.Add(movedForum);
		//			return Ok();
		//		}
		//	}
		//}
	}
	[HttpPut("{topicId,forumId}")]
	public async Task<IActionResult> MoveTopic(int topicId, int forumId)
	{
		Topic movedTopic = new Topic(){Id = topicId};
		_dbContext.Attach(movedTopic);
		movedTopic.ForumId = forumId;
		return await _dbContext.SaveChangesAsync() == 0 ? BadRequest() : Ok();
		
		//if(movedTopic != null && targetForum != null)
		//{
		//	movedTopic.Forum.Topics.Remove(movedTopic);
		//	movedTopic.Forum = targetForum;
		//	targetForum.Topics.Add(movedTopic);

		//	return Ok();
		//}
		//return BadRequest();
	}
	[HttpPut("{postId}")]
	public async Task<IActionResult> EditPost(int postId, [FromBody]string content)
	{
		DateTime editDate = DateTime.Now;
		if(ModelState.IsValid)
		{
			var postUpdate = new Post(){Id = postId};
			_dbContext.Attach(postUpdate);
			postUpdate.EditDate = editDate;
			postUpdate.Content = content;
			return await _dbContext.SaveChangesAsync() == 0 ? BadRequest() : Ok();
		}
		return BadRequest();
	}

	[HttpDelete("{type,id}")]
	public async Task<IActionResult> DeleteAsync(string type, int id)
	{
		switch(type)
		{
			case "post":
				Post p = new Post(){Id = id};
				_dbContext.Attach(p);
				_dbContext.Posts.Remove(p);
				return await _dbContext.SaveChangesAsync() == 0 ? NotFound() : Ok();
			case "topic":
				Topic t = new Topic(){ Id = id};
				_dbContext.Attach(t);
				_dbContext.Topics.Remove(t);
				return await _dbContext.SaveChangesAsync() == 0 ? NotFound() : Ok();
			case "forum":
				Forum f = new Forum(){ Id = id};
				_dbContext.Attach(f);
				_dbContext.Forums.Remove(f);
				return await _dbContext.SaveChangesAsync() == 0 ? NotFound() : Ok();
			default:
				break;
		}
		return BadRequest();
	}
}
