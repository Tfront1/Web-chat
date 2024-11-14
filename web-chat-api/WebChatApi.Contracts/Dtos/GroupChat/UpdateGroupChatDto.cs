namespace WebChatApi.Contracts.Dtos.GroupChat;

public record UpdateGroupChatDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string? Description { get; set; }
}
