using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Community_BackEnd.Data.Forums;

public class Post
{
	[Key]
	[ReadOnly(true)]
	public int Id { get; set; }
	[Required, ReadOnly(true)]
	public DateTime PostDate { get; set; } = DateTime.Now;
	public DateTime? EditDate { get; set; }
	[Required]
	public string AuthorId { get; set; }
	public int? ReplyToId { get; set; }
	[Required]
	public int TopicId { get; set; }

	[Required(AllowEmptyStrings =false)]
	public string Content { get; set; }

	//public override string ToString()
	//{
	//	return $"{Id},{Author},{TimeStamp},{TimeStampEdit},{ReplyTo},{{{Content}}}";
	//}
}
