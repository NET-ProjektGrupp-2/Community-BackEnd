using Community_BackEnd.Data;
using Community_BackEnd.Data.Forums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Nodes;

namespace Community_BackEnd.Entities;

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

    public string Role { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Token { get; set; }
}
