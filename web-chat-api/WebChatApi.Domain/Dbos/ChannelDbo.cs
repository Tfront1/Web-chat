namespace WebChatApi.Domain.Dbos;

public class ChannelDbo
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; }
	public bool IsPrivate { get; set; } = false;
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public int CreatorId { get; set; }
	public string? AvatarUrl { get; set; }

	public virtual UserDbo Creator { get; set; } = null!;
	public virtual ICollection<ChannelUserDbo> Members { get; set; } = new List<ChannelUserDbo>();
	public virtual ICollection<ChannelMessageDbo> Messages { get; set; } = new List<ChannelMessageDbo>();
}
