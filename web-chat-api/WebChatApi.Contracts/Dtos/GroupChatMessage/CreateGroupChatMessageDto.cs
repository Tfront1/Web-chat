namespace WebChatApi.Contracts.Dtos.GroupChatMessage;

public class CreateGroupChatMessageDto
{
	public int AuthorId { get; set; }
	public int GroupChatId { get; set; }
	public string Content { get; set; }
}
