using Community_BackEnd.Data;
using Microsoft.AspNetCore.Mvc;

namespace Community_BackEnd.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ForumController : ControllerBase
{
	//Get all Forum Names and Ids
	[HttpGet]
	public IEnumerable<string> Get()
	{
		return StaticDummyDB.GetForums().ConvertAll(Forum => Forum.ToString());
	}
	//Get all Topics of Forum with Id = id
	[HttpGet("{id}")]
	public IEnumerable<string> Get(int id)
	{
		return StaticDummyDB.GetForum(id).ConvertAll(Topic => Topic.ToString());
	}
	//Get all Posts of Topic with Id = id
	[HttpGet("{id}")]
	public IEnumerable<string> GetTopic(int id)
	{
		return StaticDummyDB.GetTopic(id).ConvertAll(Post => Post.ToString());
	}

	[HttpPost]
	public void Post([FromBody] string value)
	{
	}

	[HttpPut("{id}")]
	public void Put(int id, [FromBody] string value)
	{
	}

	[HttpDelete("{id}")]
	public void Delete(int id)
	{
	}
}
