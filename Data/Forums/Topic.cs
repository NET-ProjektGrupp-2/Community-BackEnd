using System.Xml.Linq;

namespace Community_BackEnd.Data.Forums;

public class Topic
{
	public int Id { get; set; }
	public string Title { get; set; }
	public List<Post> Posts	{ get; set; }

	public override string ToString()
	{
		return $"{Id},{Title}";
	}
}
