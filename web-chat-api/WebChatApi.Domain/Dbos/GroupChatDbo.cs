namespace WebChatApi.Domain.Dbos;

public class GroupChatDbo
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public int CreatorId { get; set; }
	public string? AvatarUrl { get; set; }
	public bool IsPrivate { get; set; } = false;

	public virtual UserDbo Creator { get; set; } = null!;
	public virtual ICollection<GroupChatUserDbo> Members { get; set; } = new List<GroupChatUserDbo>();
	public virtual ICollection<GroupChatMessageDbo> Messages { get; set; } = new List<GroupChatMessageDbo>();
}
