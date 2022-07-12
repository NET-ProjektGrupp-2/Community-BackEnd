using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

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
	[Required]
	public string AuthorId { get; set; }
	[Required]
	public int ForumId { get; set; }
	public List<Post> Posts	{ get; set; } = new List<Post>();

	//public override string ToString()
	//{
	//	return $"{Id},{Title}";
	//}
}
