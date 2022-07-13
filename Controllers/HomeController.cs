using Community_BackEnd.Data;
using Community_BackEnd.Data.Forums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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
	public IActionResult Edit(NewsArticle article)
	{
		DateTime editTime = DateTime.Now;
		if(ModelState.IsValid)
		{
			article.TimeStampEdit = editTime;
			StaticDummyDB.News[StaticDummyDB.News.IndexOf(StaticDummyDB.News.Find(a => a.Id == article.Id))] = article;
			return Ok();
		}
		return BadRequest();
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		NewsArticle article; ;
		if(( article = StaticDummyDB.News.Find(a => a.Id == id)  ) != default(NewsArticle))
		{
			return StaticDummyDB.News.Remove(article) ? Ok() : BadRequest();
		}
		return BadRequest();
	}
}
