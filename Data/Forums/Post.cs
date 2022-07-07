using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Community_BackEnd.Data.Forums;

public class Post
{
	[Key]
	[ReadOnly(true)]
	public int Id { get; set; }
	[Required]
	public string Author { get; set; }
	[Required, ReadOnly(true)]
	public DateTime TimeStamp { get; set; }
	public DateTime TimeStampEdit { get; set; }
	public int ReplyTo { get; set; }
	[Required]
	public Topic Topic { get; set; }
	[Required]
	public string Content { get; set; }

	public override string ToString()
	{
		return $"{Id},{Author},{TimeStamp},{TimeStampEdit},{ReplyTo},{{{Content}}}";
	}
}
