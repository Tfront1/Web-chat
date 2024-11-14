namespace WebChatApi.Contracts.Dtos.GroupChat;

public record CreateGroupChatDto
{
	public string Name { get; set; }
	public string? Description { get; set; }
	public int CreatorId { get; set; }
}
