using Community_BackEnd.Data.Forums;

namespace Community_BackEnd.Data;

public class StaticDummyDB
{
	static List<Post> Posts = new List<Post>(new Post[]{
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

	static List<Topic> Topics = new List<Topic>(new Topic[]{new Topic(){
		Id=1,
		Posts=new(Posts),
		Title="Hello World!"
	}});

	static List<Forum> Forums = new(new Forum[]{new Forum(){
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
}
