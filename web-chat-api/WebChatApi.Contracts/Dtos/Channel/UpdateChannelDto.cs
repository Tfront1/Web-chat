namespace WebChatApi.Contracts.Dtos.Channel;

public record UpdateChannelDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string? Description { get; set; }
}
