namespace WebChatApi.Contracts.Dtos.Channel;

public class ChannelUserDto
{
	public int ChannelId { get; set; }
	public int UserId { get; set; }
	public bool? IsAdmin { get; set; }
}
