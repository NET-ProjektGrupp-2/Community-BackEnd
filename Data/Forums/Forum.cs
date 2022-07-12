using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

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

	public int? ParentForumId { get; set; }
	public List<Forum>? SubForums { get; set; }
	public List<Topic> Topics { get; set; } = new List<Topic>();

	public List<AppUser> Moderators { get; set; }
	public string RestrictedRoleId { get; set; }

	//public override string ToString()
	//{
	//	return $"{Id},{Name}";
	//}
}
