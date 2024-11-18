namespace WebChatApi.Domain.Dbos;

public class GroupChatMessageDbo
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
	public int GroupChatId { get; set; }
	public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsEdited { get; set; }
    public DateTime? EditedAt { get; set; }

    public virtual UserDbo Author { get; set; }
    public virtual GroupChatDbo GroupChat { get; set; }
}
