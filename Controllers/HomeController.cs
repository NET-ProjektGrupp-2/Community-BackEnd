using Community_BackEnd.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace Community_BackEnd.Controllers;
[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{

	[HttpGet]
	public IEnumerable<NewsArticle> Get()
	{
		return StaticDummyDB.GetNews();
		//return StaticDummyDB.GetNews().ConvertAll(article => article.ToString());
	}

	[HttpGet("{id}")]
	public NewsArticle Get(int id)
	{
		return StaticDummyDB.GetArticle(id);
	}

	[Authorize("Moderator")]
	[HttpPost]
	public IActionResult Post(NewsArticle article)
	{
		if(ModelState.IsValid)
		{
			StaticDummyDB.News.Add(article);
			return Ok();
		}
		return BadRequest();
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
