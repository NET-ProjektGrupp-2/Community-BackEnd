using Community_BackEnd.Data;
using Community_BackEnd.Data.Forums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Community_BackEnd.Controllers;
[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
	private AppDbContext _dbContext;
	public HomeController(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	[HttpGet]
	public async Task<IActionResult> Get(int? after)
	{
		var articles = await _dbContext.News.AsNoTracking().Skip(after??0).Take(5).Include(a => a.Author).ToListAsync();
		var authors = new List<List<String>>();
		articles.ConvertAll(
			a => {
				if(a.Author != null && !authors.Any(author => author[0] == a.Author.Id)) 
				{	authors.Add(a.Author.StripInfo()); }
				a.Author = null;
				return a;
			});
		return new JsonResult(
			new {articles,authors}, 
			new JsonSerializerOptions(JsonSerializerDefaults.Web) { 
				DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
		});
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> Read(int id)
	{
		var article = await _dbContext.News.FindAsync(id);
		return article == null ? NotFound() : Ok(article);
	}

	//[Authorize("Writer")]
	[HttpPost]
	public async Task<IActionResult> Post(NewsArticle article)
	{
		if(ModelState.IsValid)
		{
			_dbContext.News.Add(article);
			return await _dbContext.SaveChangesAsync() == 0
				? BadRequest() : Ok(article);
		}
		return BadRequest();
	}

	//[Authorize("Writer")] // Or article author == user ??
	[HttpPut("edit")]
	public async Task<IActionResult> Edit(int articleId, string content)
	{
		if(articleId < 1)
		{
			return BadRequest();
		}
		DateTime editDate = DateTime.Now;
		EntityEntry<NewsArticle>? article = _dbContext.ChangeTracker.Entries<NewsArticle>()
			.FirstOrDefault(x => x.Entity.Id == articleId);
		if(article == null)
		{
			article = _dbContext.Attach(new NewsArticle() { Id = articleId });
		}
		article.Entity.EditDate = editDate;
		article.Entity.Content = content;
		return await _dbContext.SaveChangesAsync() == 0
			? NotFound() : Ok(new { editTime = editDate.ToLongDateString() });
	}

	//[Authorize("Moderator,Administrator")]
	[HttpDelete("delete")]
	public async Task<IActionResult> Delete([Required] int id)
	{
		NewsArticle article = new NewsArticle(){Id = id};
		_dbContext.Attach(article);
		_dbContext.News.Remove(article);
		return await _dbContext.SaveChangesAsync() == 0
			? NotFound() : Ok();
	}
}
