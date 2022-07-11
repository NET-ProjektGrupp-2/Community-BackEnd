using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace Community_BackEnd.Data.Forums;

public class Forum
{
	[Key]
	[ReadOnly(true)]
	public int Id { get; set; }
	[Required(AllowEmptyStrings = false)]
	public string Name { get; set; }
	[Required(AllowEmptyStrings = false)]
	public string Description { get; set; }

	public Forum? ParentForum { get; set; }
	public List<Forum> SubForums { get; set; } = new List<Forum>();
	public List<Topic> Topics { get; set; } = new List<Topic>();

	public List<AppUser> Moderators { get; set; }
	public IdentityRole? Restricted { get; set; }

	public override string ToString()
	{
		return $"{Id},{Name}";
	}
}
