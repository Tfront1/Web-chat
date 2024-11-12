namespace WebChatApi.Domain.Dbos;

public class ChannelUserDbo
{
	public int ChannelId { get; set; }
	public int UserId { get; set; }
	public bool IsAdmin { get; set; } = false;

	public virtual ChannelDbo Channel { get; set; } = null!;
	public virtual UserDbo User { get; set; } = null!;
}
