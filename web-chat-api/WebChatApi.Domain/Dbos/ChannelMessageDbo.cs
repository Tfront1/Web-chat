namespace WebChatApi.Domain.Dbos;

public class ChannelMessageDbo
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? AttachmentUrl { get; set; }
    public bool IsEdited { get; set; }
    public DateTime? EditedAt { get; set; }
    public int ChannelId { get; set; }

    public virtual UserDbo Author { get; set; }
    public virtual ChannelDbo Channel { get; set; }
}
