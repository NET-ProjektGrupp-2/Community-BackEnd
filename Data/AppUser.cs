using Community_BackEnd.Data.Forums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Nodes;

namespace Community_BackEnd.Data;

public class AppUser : IdentityUser
{
	public AppUser(string userName) : base(userName)
	{
	}
	[Required]
	public string DisplayName { get; set; }
	[Required(AllowEmptyStrings = false)]
	public string Firstname { get; set; }
	[Required(AllowEmptyStrings = false)]
	public string Surname { get; set; }
	public bool HidePersonalInfo { get; set; } = true;
	[Required]
	public DateTime CreationDate { get; set; } = DateTime.Now;
	// JSON { Interests : 'Turtles, Code', Bio : 'Tldr;' }
	public string? AboutMe { get; set; }
	public List<Forum>? ModeratedForums { get; set; }
	public List<Topic>? Topics { get; set; }
	public List<Post>? Posts { get; set; }
	public List<NewsArticle>? Articles { get; set; }
	public List<IdentityUserRole<string>> IdentityUserRoles { get; set; }

	internal List<string> StripInfo()
	{
		var strippedUser = new List<string>() {
			Id,
			DisplayName,
			CreationDate.ToString("g")
		};
		if(!this.HidePersonalInfo)
		{
			strippedUser.Add(Firstname);
			strippedUser.Add(Surname);
			strippedUser.Add(Email);
			strippedUser.Add(AboutMe);
		}

		return strippedUser;
	}
}
