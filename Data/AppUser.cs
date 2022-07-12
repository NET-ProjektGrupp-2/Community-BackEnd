using Community_BackEnd.Data.Forums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Community_BackEnd.Data;

public class AppUser : IdentityUser
{
	public AppUser(string userName) : base(userName)
	{
	}

	public string DisplayName { get; set; }
	[Required(AllowEmptyStrings = false)]
	public string Firstname { get; set; }
	[Required(AllowEmptyStrings = false)]
	public string Surname { get; set; }
	public bool HidePersonalInfo { get; set; } = true;
	[Required]
	public DateTime CreationDate { get; set; } = DateTime.Now;
	public Dictionary<string,string>? AboutMe { get; set; }
	public List<Forum>? ModeratedForums { get; set; }
	public List<Topic>? Topics { get; set; }
	public List<Post>? Posts { get; set; }

}
