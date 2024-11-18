namespace WebChatApi.Contracts.Dtos.PersonalMessage;

public class GetPersonalMessagesByUsersDto
{
	public int AuthorId { get; set; }
	public int RecipientId { get; set; }
}
