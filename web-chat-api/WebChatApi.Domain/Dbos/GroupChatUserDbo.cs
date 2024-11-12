namespace WebChatApi.Domain.Dbos;

public class GroupChatUserDbo
{
	public int GroupChatId { get; set; }
	public int UserId { get; set; }

	public virtual GroupChatDbo GroupChat { get; set; } = null!;
	public virtual UserDbo User { get; set; } = null!;
}