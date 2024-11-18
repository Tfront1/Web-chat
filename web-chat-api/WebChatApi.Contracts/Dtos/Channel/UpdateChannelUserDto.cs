namespace WebChatApi.Contracts.Dtos.Channel;

public class UpdateChannelUserDto
{
	public int ChannelId { get; set; }
	public int UserId { get; set; }
	public bool IsAdmin { get; set; }
}
