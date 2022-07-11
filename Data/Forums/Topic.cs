using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;

namespace Community_BackEnd.Data.Forums;

public class Topic
{
	[Key]
	[ReadOnly(true)]
	public int Id { get; set; }
	[Required(AllowEmptyStrings = false)]
	public string Title { get; set; }
	[Required]
	public AppUser Author { get; set; }
	public List<Post> Posts	{ get; set; } = new List<Post>();
	[Required]
	public Forum Forum { get; set; }

	public override string ToString()
	{
		return $"{Id},{Title}";
	}
}
