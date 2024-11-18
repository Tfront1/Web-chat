namespace WebChatApi.Contracts.Dtos.GroupChatMessage;

public class GroupChatMessageDto
{
	public int Id { get; set; }
	public int AuthorId { get; set; }
	public int GroupChatId { get; set; }
	public string Content { get; set; }
	public DateTime CreatedAt { get; set; }
	public bool IsEdited { get; set; }
	public DateTime? EditedAt { get; set; }
}
