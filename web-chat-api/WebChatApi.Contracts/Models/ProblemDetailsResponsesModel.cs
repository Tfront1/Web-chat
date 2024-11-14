namespace WebChatApi.Contracts.Models;

public static class ProblemDetailsResponsesModel
{
	public static readonly List<string> ProblemDetailsResponses =
	[
		EntityNotFound,
		EntityCreateFailed,
		EntityUpdateFailed,
		EntityDeleteFailed,
		UserAlreadyExists,
		UserNotCreated,
		UserNotFound,
		UserNotUpdated,
		GroupChatNotCreated,
		GroupChatNotFound,
		GroupChatNotUpdated,
		ChannelNotCreated,
		ChannelNotFound,
		ChannelNotUpdated,
	];

	public const string EntityNotFound = "Entity not found.";
	public const string EntityCreateFailed = "Entity create failed.";
	public const string EntityUpdateFailed = "Entity update failed.";
	public const string EntityDeleteFailed = "Entity delete failed.";
	public const string UserAlreadyExists = "User already exists.";
	public const string UserNotCreated = "User not created.";
	public const string UserNotFound = "User not found.";
	public const string UserNotDeleted = "User not deleted.";
	public const string UserNotUpdated = "User not updated.";
	public const string GroupChatNotCreated = "Group chat not created.";
	public const string GroupChatNotFound = "Group chat not found.";
	public const string GroupChatNotUpdated = "Group chat not updated.";
	public const string GroupChatNotDeleted = "Group chat not deleted.";
	public const string ChannelNotCreated = "Channel not created.";
	public const string ChannelNotFound = "Channel not found.";
	public const string ChannelNotUpdated = "Channel not updated.";
	
}

