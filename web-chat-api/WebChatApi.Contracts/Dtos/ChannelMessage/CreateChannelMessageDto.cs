namespace WebChatApi.Contracts.Dtos.ChannelMessage;

public class CreateChannelMessageDto
{
	public int AuthorId { get; set; }
	public int ChannelId { get; set; }
	public string Content { get; set; }
}
