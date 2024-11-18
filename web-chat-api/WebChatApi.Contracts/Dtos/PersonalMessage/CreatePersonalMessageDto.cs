namespace WebChatApi.Contracts.Dtos.PersonalMessage;

public class CreatePersonalMessageDto
{
	public int AuthorId { get; set; }
	public int RecipientId { get; set; }
	public string Content { get; set; }

}
