using Community_BackEnd.Data.Forums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Community_BackEnd.Data;

public class NewsArticle
{
	[Key]
	[ReadOnly(true)]
	public int Id { get; set; }
	[Required]
	public string Title { get; set; }
	[Required, ReadOnly(true)]
	public string Author { get; set; }
	[Required, ReadOnly(true)]
	public DateTime PublishDate { get; set; } = DateTime.Now;
	public Topic DiscussionThread { get; set; }
	[Required]
	public string Content { get; set; }

	public override string ToString()
	{
		return $"{Id},{Title},{Author},{PublishDate},{DiscussionThread.Id},{Content}";
	}
}
