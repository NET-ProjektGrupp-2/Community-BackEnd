using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Community_BackEnd.Data.Forums;

public class Post
{
	[Key]
	[ReadOnly(true)]
	public int Id { get; set; }
	[Required]
	public AppUser Author { get; set; }
	[Required, ReadOnly(true)]
	public DateTime TimeStamp { get; set; } = DateTime.Now;
	public DateTime TimeStampEdit { get; set; }
	public Post? ReplyTo { get; set; }
	[Required]
	public Topic Topic { get; set; }

	[Required(AllowEmptyStrings =false)]
	public string Content { get; set; }

	public override string ToString()
	{
		return $"{Id},{Author},{TimeStamp},{TimeStampEdit},{ReplyTo},{{{Content}}}";
	}
}
