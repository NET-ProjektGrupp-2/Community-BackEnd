using Community_BackEnd.Data.Forums;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Numerics;
using System;

namespace Community_BackEnd.Data;

public class StaticDummyDB
{
	#region Forum
	public static List<Post> Posts = new List<Post>(new Post[]{
		new Post(){
			Id=1,
			Author="Jens",
			TimeStamp=DateTime.Now.AddMinutes(-4),
			Content="Hello World!"
		},
		new Post(){
			Id=2,
			Author="Jens",
			TimeStamp=DateTime.Now.AddMinutes(-3),
			ReplyTo=1,
			Content="Well met World!"
		},
		new Post(){
			Id=3,
			Author="Jens",
			TimeStamp=DateTime.Now.AddMinutes(-2),
			ReplyTo=2,
			Content="How is the World weather?"
		},
		new Post(){
			Id=4,
			Author="Jens",
			TimeStamp=DateTime.Now.AddMinutes(-1),
			ReplyTo=3,
			Content="The sun is shining World!"
		}
	});
	public static List<Topic> Topics = new List<Topic>(new Topic[]{new Topic(){
		Id=1,
		Posts=new(Posts),
		Title="Hello World!"
	}});
	public static List<Forum> Forums = new(new Forum[]{new Forum(){
		Id=1, Name="Main",
		Topics=new(Topics)
	}});

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

	public static List<NewsArticle> News = new List<NewsArticle>(new NewsArticle[]{
		new NewsArticle{
			Id=1, 
			Title="There are News!", 
			Author="Jens", 
			DiscussionThread=Topics[0],
			Content="There are now news in the news feed. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut felis ante, consequat ac urna nec, pretium consequat diam. Donec imperdiet bibendum est sed luctus. Sed pretium, eros imperdiet laoreet dapibus, neque ante molestie metus, nec sagittis metus est ut velit. Nullam ut egestas diam. Curabitur accumsan diam ac lorem commodo, vitae condimentum ipsum porta. Duis ullamcorper, enim non hendrerit egestas, nunc libero auctor turpis, consequat accumsan ligula lacus eget dolor. Etiam tortor arcu, laoreet in iaculis ac, facilisis non libero. Morbi egestas massa mi, ut dictum tellus sagittis in. Morbi nec mattis elit. Aenean sit amet nisi viverra, dignissim magna."
		},
		new NewsArticle{
			Id=2,
			Title="Lorem Ipsum",
			Author="Lorem",
			DiscussionThread=Topics[0],
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
}
