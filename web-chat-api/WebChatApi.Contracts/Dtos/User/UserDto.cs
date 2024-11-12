namespace WebChatApi.Contracts.Dtos.User;

public class UserDto
{
	public int Id { get; set; }
	public string Username { get; set; }
	public string? AvatarUrl { get; set; }
	public decimal SubscriptionAmount { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public bool IsActive { get; set; }
	public List<string> Stack { get; set; }
	public string City { get; set; }
	public string? Description { get; set; }
	public DateTime LastOnline { get; set; }
	public string? CurrentStatus { get; set; }
	public string? Email { get; set; }
	public string? PhoneNumber { get; set; }
	public string? GitHubUrl { get; set; }
	public string? LinkedInUrl { get; set; }
}
