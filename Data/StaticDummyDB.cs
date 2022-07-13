using Community_BackEnd.Data.Forums;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Numerics;
using System;
using Microsoft.AspNetCore.Identity;

namespace Community_BackEnd.Data;

public class StaticDummyDB
{
	#region Users
	public static List<IdentityRole> Roles = new List<IdentityRole>(
		new IdentityRole[]{
			new IdentityRole("Administrator"),
			new IdentityRole("Moderator"),
			new IdentityRole("Writer"),
			new IdentityRole("User")
		});

	public static List<AppUser> Users = new List<AppUser>(
		new AppUser[]{
			new AppUser("jeres89"){
				DisplayName="Jens",
				Firstname="Jens",
				Surname="Eresund",
				Email="jens.eresund@gmail.com"
			},
			new AppUser("kembAB"){
				DisplayName="Abel",
				Firstname="Abel",
				Surname="Magicho",
				Email="kokiabel1986@gmail.com"
			},
			new AppUser("Libre255"){
				DisplayName="Brian",
				Firstname="Brian",
				Surname="Veliz"
			},
			new AppUser("UnboundKey"){
				DisplayName="Benjamin",
				Firstname="Benjamin",
				Surname="Nordin",
				Email="godiset@gmail.com"
			},
		});

	public static List<IdentityUserRole<string>> UserRoles = new (
		new IdentityUserRole<string>[] {
			new IdentityUserRole<string>() {
				RoleId = Roles[0].Id,
				UserId = Users[0].Id
			},
			new IdentityUserRole<string>() {
				RoleId = Roles[0].Id,
				UserId = Users[1].Id
			},
			new IdentityUserRole<string>() {
				RoleId = Roles[0].Id,
				UserId = Users[2].Id
			},
			new IdentityUserRole<string>() {
				RoleId = Roles[0].Id,
				UserId = Users[3].Id
			}
		});
	#endregion
	#region Forum
	public static List<Post> Posts = new List<Post>(
		new Post[]{
			new Post(){
				Id=1,
				PostDate=DateTime.Now.AddMinutes(-4),
				AuthorId=Users[0].Id,
				TopicId=1,
				Content="Hello World!"
			},
			new Post(){
				Id=2,
				PostDate=DateTime.Now.AddMinutes(-3),
				AuthorId=Users[1].Id,
				ContextPostId=1,
				TopicId=1,
				Content="Well met World!"
			},
			new Post(){
				Id=3,
				PostDate=DateTime.Now.AddMinutes(-2),
				AuthorId=Users[2].Id,
				ContextPostId=2,
				TopicId=1,
				Content="How is the World weather?"
			},
			new Post(){
				Id=4,
				PostDate=DateTime.Now.AddMinutes(-1),
				AuthorId=Users[3].Id,
				ContextPostId=3,
				TopicId=1,
				Content="The sun is shining, World!"
			}
		});
	public static List<Topic> Topics = new List<Topic>(
		new Topic[]{
			new Topic(){
				Id=1,
				Title="Hello World!",
				AuthorId=Users[0].Id,
				ForumId=1
			}
		});
	public static List<Forum> Forums = new(
		new Forum[]{
			new Forum(){
				Id=1,
				Name="Main",
				Description="The forum of all forums!"
			}
		});

	internal static List<Forum> GetForums()
	{
		return Forums;
	}
	internal static List<Topic> GetForum(int id)
	{
		Forum? target = Forums.Find(Forum => Forum.Id == id);
		if(target != null)
		{
			return target.Topics;
		}
		return null;
	}
	internal static List<Post> GetTopic(int id)
	{
		Topic? target = Topics.Find(Topic => Topic.Id == id);
		if(target != null)
		{
			return target.Posts;
		}
		return null;
	}
	#endregion
	#region Articles
	public static List<NewsArticle> News = new List<NewsArticle>(
		new NewsArticle[]{
			new NewsArticle{
				Id=1,
				Title="There are News!",
				AuthorId=Users[0].Id,
				TopicId=1,
				Content="There are now news in the news feed. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut felis ante, consequat ac urna nec, pretium consequat diam. Donec imperdiet bibendum est sed luctus. Sed pretium, eros imperdiet laoreet dapibus, neque ante molestie metus, nec sagittis metus est ut velit. Nullam ut egestas diam. Curabitur accumsan diam ac lorem commodo, vitae condimentum ipsum porta. Duis ullamcorper, enim non hendrerit egestas, nunc libero auctor turpis, consequat accumsan ligula lacus eget dolor. Etiam tortor arcu, laoreet in iaculis ac, facilisis non libero. Morbi egestas massa mi, ut dictum tellus sagittis in. Morbi nec mattis elit. Aenean sit amet nisi viverra, dignissim magna."
			},
			new NewsArticle{
				Id=2,
				Title="Lorem Ipsum",
				AuthorId=Users[1].Id,
				TopicId=1,
				Content="Ipsum dolor sit amet, consectetur adipiscing elit. Ut felis ante, consequat ac urna nec, pretium consequat diam. Donec imperdiet bibendum est sed luctus. Sed pretium, eros imperdiet laoreet dapibus, neque ante molestie metus, nec sagittis metus est ut velit. Nullam ut egestas diam. Curabitur accumsan diam ac lorem commodo, vitae condimentum ipsum porta. Duis ullamcorper, enim non hendrerit egestas, nunc libero auctor turpis, consequat accumsan ligula lacus eget dolor. Etiam tortor arcu, laoreet in iaculis ac, facilisis non libero. Morbi egestas massa mi, ut dictum tellus sagittis in. Morbi nec mattis elit. Aenean sit amet nisi viverra, dignissim magna."
			}
		});

	internal static List<NewsArticle> GetNews()
	{
		return News;
	}
	internal static NewsArticle GetArticle(int id)
	{
		return News.Find(article => article.Id == id);
	}
	#endregion

}
