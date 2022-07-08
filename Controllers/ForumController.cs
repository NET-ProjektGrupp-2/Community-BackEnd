using Community_BackEnd.Data;
using Community_BackEnd.Data.Forums;
using Microsoft.AspNetCore.Mvc;

namespace Community_BackEnd.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ForumController : ControllerBase
{
	//Get all Forum Names and Ids
	//ToDo: add counts of topics, ?
	[HttpGet]
	public IEnumerable<Forum> Get()
	{
		return StaticDummyDB.GetForums();
		//return StaticDummyDB.GetForums().ConvertAll(Forum => Forum.ToString());
	}
	//Get all Topics of Forum with Id = id
	[HttpGet("{id}")]
	public IEnumerable<Topic> Get(int id)
	{
		return StaticDummyDB.GetForum(id);
		//return StaticDummyDB.GetForum(id).ConvertAll(Topic => Topic.ToString());
	}
	//Get all Posts of Topic with Id = id
	[HttpGet("{id}")]
	public IEnumerable<Post> GetTopic(int id)
	{
		return StaticDummyDB.GetTopic(id);
		//return StaticDummyDB.GetTopic(id).ConvertAll(Post => Post.ToString());
	}
	[HttpPost]
	public IActionResult Post(Forum forum)
	{
		if(ModelState.IsValid)
		{
			StaticDummyDB.Forums.Add(forum);
			return Ok();
		}
		return BadRequest();
	}
	[HttpPost]
	public IActionResult Post(Topic topic)
	{
		if(ModelState.IsValid)
		{
			StaticDummyDB.Topics.Add(topic);
			return Ok();
		}
		return BadRequest();
	}
	[HttpPost]
	public IActionResult Post(Post post)
	{
		if(ModelState.IsValid)
		{
			StaticDummyDB.Posts.Add(post);
			return Ok();
		}
		return BadRequest();
	}
	[HttpPut("{forumId,targetForumId}")]
	public /*async Task<IActionResult>*/IActionResult MoveForum(int forumId, int targetForumId)
	{
		Forum movedForum = StaticDummyDB.Forums.Find(forum => forum.Id == forumId);
		if(movedForum != null)
		{
			if(targetForumId == 0)
			{
				movedForum.ParentForum = null;
				return Ok();
			}
			else
			{
				Forum targetForum = StaticDummyDB.Forums.Find(forum => forum.Id == targetForumId);
				if(targetForum != null)
				{
					if(movedForum.ParentForum != null)
					{
						movedForum.ParentForum.SubForums.Remove(movedForum);
					}
					movedForum.ParentForum = targetForum;
					targetForum.SubForums.Add(movedForum);
					return Ok();
				}
			}
		}
		return BadRequest();
	}
	[HttpPut("{topicId,forumId}")]
	public /*async Task<IActionResult>*/IActionResult MoveTopic(int topicId, int forumId)
	{
		Topic movedTopic = StaticDummyDB.Topics.Find(topic => topic.Id == topicId);
		Forum targetForum = StaticDummyDB.Forums.Find(forum => forum.Id == forumId);
		if(movedTopic != null && targetForum != null)
		{
			movedTopic.Forum.Topics.Remove(movedTopic);
			movedTopic.Forum = targetForum;
			targetForum.Topics.Add(movedTopic);

			return Ok();
		}
		return BadRequest();
	}
	[HttpPut("{id}")]
	public /*async Task<IActionResult>*/IActionResult Edit(Post post)
	{
		DateTime editTime = DateTime.Now;
		if(ModelState.IsValid)
		{
			post.TimeStampEdit = editTime;

			StaticDummyDB.Posts[StaticDummyDB.Posts.IndexOf(StaticDummyDB.Posts.Find(p => p.Id == post.Id))] = post;
			//if(await TryUpdateModelAsync<Post>(post))
			//{
				return Ok();
			//}
		}
		return BadRequest();
	}

	[HttpDelete("{type,id}")]
	public IActionResult Delete(string type, int id)
	{
		bool success = false;
		switch(type)
		{
			case "post":
				Post p;
				if((p = StaticDummyDB.Posts.Find(post => post.Id == id)) != default(Post))
				{
					success = StaticDummyDB.Posts.Remove(p);
				}
				break;
			case "topic":
				Topic t;
				if((t = StaticDummyDB.Topics.Find(topic => topic.Id == id) ) != default(Topic))
				{
					success = StaticDummyDB.Topics.Remove(t);
				}
				break;
			case "forum":
				Forum f;
				if(( f = StaticDummyDB.Forums.Find(forum => forum.Id == id) ) != default(Forum))
				{
					success = StaticDummyDB.Forums.Remove(f);
				}
				break;
			default:
				break;
		}
		return success ? Ok() : BadRequest();
	}
}
