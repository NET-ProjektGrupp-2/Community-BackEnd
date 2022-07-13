using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Community_BackEnd.Data.Forums;

public class Post
{
	[Key, ReadOnly(true)]
	public int Id { get; set; }
	[Required, ReadOnly(true)]
	public DateTime PostDate { get; set; } = DateTime.Now;
	public DateTime? EditDate { get; set; }
	[BindRequired]
	public string? AuthorId { get; set; }
	public AppUser? Author { get; set; }
	public int? ContextPostId { get; set; }
	public Post? ContextPost { get; set; }
	public List<Post>? Replies { get; set; }
	[Required]
	public int TopicId { get; set; }
	public Topic Topic { get; set; }
	[Required(AllowEmptyStrings =false)]
	public string Content { get; set; }

	//public override string ToString()
	//{
	//	return $"{Id},{Author},{TimeStamp},{TimeStampEdit},{ReplyTo},{{{Content}}}";
	//}
}
