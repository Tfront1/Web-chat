using System.Threading.Channels;
using WebChatApi.Domain.Dbos;

public class UserDbo
{
	public int Id { get; set; }
	public string Username { get; set; } = string.Empty;
	public string? AvatarUrl { get; set; }
	public decimal SubscriptionAmount { get; set; } = 0;
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public bool IsActive { get; set; } = true;
	public List<string> Stack { get; set; } = new List<string>();
	public string City { get; set; } = string.Empty;
	public string? Description { get; set; }
	public DateTime LastOnline { get; set; } = DateTime.UtcNow;
	public string? CurrentStatus { get; set; }
	public string? Email { get; set; }
	public string? PhoneNumber { get; set; }
	public string? GitHubUrl { get; set; }
	public string? LinkedInUrl { get; set; }

	public virtual ICollection<ChannelUserDbo> ChannelsMember { get; set; } = new List<ChannelUserDbo>();
	public virtual ICollection<GroupChatUserDbo> GroupChatsMember { get; set; } = new List<GroupChatUserDbo>();
}