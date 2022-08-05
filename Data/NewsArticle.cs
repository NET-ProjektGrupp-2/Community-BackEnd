using Community_BackEnd.Data.Forums;
using Community_BackEnd.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Community_BackEnd.Data;

public class NewsArticle
{
	[Key, ReadOnly(true)]
	public int Id { get; set; }

	[Required(AllowEmptyStrings = false)]
	public string Title { get; set; }

	[BindRequired, ReadOnly(true)]
	public string? AuthorId { get; set; }
	public AppUser? Author { get; set; }

	[Required(AllowEmptyStrings = false)]
	public string Content { get; set; }
	[BindRequired]
	public int? TopicId { get; set; }
	public Topic? Topic { get; set; }
	//Json object with a category names array
	public string Categories { get; set; } = @"{ Categories: ['All', 'News'] }";

	[Required, ReadOnly(true)]
	public DateTime PublishDate { get; set; } = DateTime.Now;
	public DateTime? EditDate { get; set; }

	//public override string ToString()
	//{
	//	return $"{Id},{Title},{Author},{PublishDate},{DiscussionThread.Id},{Content}";
	//}
}
