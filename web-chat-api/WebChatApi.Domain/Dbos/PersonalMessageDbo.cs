namespace WebChatApi.Domain.Dbos;

public class PersonalMessageDbo
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
	public int RecipientId { get; set; }
	public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsEdited { get; set; }
    public DateTime? EditedAt { get; set; }

    public virtual UserDbo Author { get; set; }
    public virtual UserDbo Recipient { get; set; }
}