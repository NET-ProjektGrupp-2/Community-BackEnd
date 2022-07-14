using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Community_BackEnd.Data.Forums;

public class Forum
{
	[Key]
	[ReadOnly(true)]
	public int Id { get; set; }
	[Required]
	public string Name { get; set; }
	[Required]
	public string Description { get; set; }

	public Forum? ParentForum { get; set; }
    public List<Forum> SubForums { get; set; } = new List<Forum>();
	public List<Topic> Topics { get; set; } = new List<Topic>();

	public override string ToString()
	{
		return $"{Id},{Name}";
	}
}
