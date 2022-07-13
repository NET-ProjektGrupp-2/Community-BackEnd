using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Community_BackEnd.Data.Forums;

public class Topic
{
	[Key]
	[ReadOnly(true)]
	public int Id { get; set; }
	[Required(AllowEmptyStrings = false)]
	public string Title { get; set; }
	[Required]
	public DateTime CreationDate { get; set; }
	public NewsArticle? Article { get; set; }
	[BindRequired]
	public string? AuthorId { get; set; }
	public AppUser? Author { get; set; }
	[BindRequired]
	public int ForumId { get; set; }
	public Forum Forum { get; set; }
	public List<Post> Posts	{ get; set; }

	//public override string ToString()
	//{
	//	return $"{Id},{Title}";
	//}
}
