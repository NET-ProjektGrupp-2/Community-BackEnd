namespace Community_BackEnd.Data.Forums;

public class Post
{
	public int Id { get; set; }
	public string Author { get; set; }
	public DateTime TimeStamp { get; set; }
	public DateTime TimeStampEdit { get; set; }
	public int ReplyTo { get; set; }
	public string Content { get; set; }
	public override string ToString()
	{
		return $"{Id},{Author},{TimeStamp},{TimeStampEdit},{ReplyTo},{{{Content}}}";
	}
}
