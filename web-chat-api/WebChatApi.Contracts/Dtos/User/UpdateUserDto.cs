namespace WebChatApi.Contracts.Dtos.User;

public record UpdateUserDto
{
	public int Id { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string City { get; set; }
	public string? Description { get; set; }
	public string? Email { get; set; }
	public string? PhoneNumber { get; set; }
	public string? GitHubUrl { get; set; }
	public string? LinkedInUrl { get; set; }
}
