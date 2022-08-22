using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Identity;
using Community_BackEnd.Entities;

namespace Community_BackEnd.Data.Forums;

public class Forum
{
	[Key, ReadOnly(true)]
	public int Id { get; set; }
	[Required(AllowEmptyStrings = false)]
	public string Name { get; set; }
	[Required(AllowEmptyStrings = false)]
	public string Description { get; set; }

	public int? ParentForumId { get; set; }
	public Forum? ParentForum { get; set; }
	public List<Forum>? SubForums { get; set; }
	public List<Topic>? Topics { get; set; }

	public List<User>? Moderators { get; set; }
	public IdentityRole? RestrictedRole { get; set; }
	public string? RestrictedRoleId { get; set; }

	//public override string ToString()
	//{
	//	return $"{Id},{Name}";
	//}
}
