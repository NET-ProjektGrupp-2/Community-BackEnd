namespace Community_BackEnd.Data.Forums;

public class Forum
{
	public int Id { get; set; }

	public string Name { get; set; }

	public Forum? ParentForum { get; set; }
	public List<Forum> SubForums { get; set; } = new List<Forum>();

	public List<Topic> Topics { get; set; } = new List<Topic>();

	public override string ToString()
	{
		return $"{Id},{Name}";
	}
}
