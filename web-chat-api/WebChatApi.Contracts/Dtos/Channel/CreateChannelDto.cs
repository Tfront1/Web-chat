namespace WebChatApi.Contracts.Dtos.Channel;

public record class CreateChannelDto
{
	public string Name { get; set; }
	public string? Description { get; set; }
	public int CreatorId { get; set; }
}
