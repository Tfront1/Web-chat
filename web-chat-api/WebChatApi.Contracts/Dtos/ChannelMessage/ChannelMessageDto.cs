namespace WebChatApi.Contracts.Dtos.ChannelMessage;

public class ChannelMessageDto
{
	public int Id { get; set; }
	public int AuthorId { get; set; }
	public int ChannelId { get; set; }
	public string Content { get; set; }
	public DateTime CreatedAt { get; set; }
	public bool IsEdited { get; set; }
	public DateTime? EditedAt { get; set; }
}
