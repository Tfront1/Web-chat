namespace WebChatApi.Contracts.Dtos.GroupChat;

public class GroupChatDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string? Description { get; set; }
	public DateTime CreatedAt { get; set; }
	public int CreatorId { get; set; }
	public string? AvatarUrl { get; set; }
}
