using Community_BackEnd.Data.Forums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Community_BackEnd.Data;

public class NewsArticle
{
	[Key, ReadOnly(true)]
	public int Id { get; set; }

	[Required(AllowEmptyStrings = false)]
	public string Title { get; set; }

	[Required, ReadOnly(true)]
	public AppUser Author { get; set; }

	[Required(AllowEmptyStrings = false)]
	public string Content { get; set; }
	public Topic DiscussionThread { get; set; }
	public List<string> Categories { get; set; }

	[Required, ReadOnly(true)]
	public DateTime PublishDate { get; set; } = DateTime.Now;
	public DateTime TimeStampEdit { get; set; }

	public override string ToString()
	{
		return $"{Id},{Title},{Author},{PublishDate},{DiscussionThread.Id},{Content}";
	}
}
