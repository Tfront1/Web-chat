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
		GroupChatUserNotCreated,
		GroupChatUserNotDeleted,
		GroupChatMessageNotCreated,
		GroupChatMessageNotUpdated,
		GroupChatMessageNotFound,
		GroupChatUserNotFound,
		GroupChatUserAlreadyExist,
		ChannelNotCreated,
		ChannelNotFound,
		ChannelNotUpdated,
		ChannelUserAlreadyExist,
		ChannelUserNotCreated,
		ChannelUserNotFound,
		ChannelUserNotDeleted,
		ChannelUserRoleAlreadySame,
		ChannelUserRoleNotUpdated,
		ChannelMessageNotCreated,
		ChannelMessageNotUpdated,
		ChannelMessageNotFound,
		AuthorNotFound,
		RecipientNotFound,
		PersonalMessageNotCreated,
		PersonalMessageNotUpdated,
		PersonalMessageNotFound,
		MessageContentAlreadySame,
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
	public const string GroupChatUserNotCreated = "Group chat user not created.";
	public const string GroupChatUserNotDeleted = "Group chat user not deleted.";
	public const string GroupChatUserNotFound = "Group chat user not found.";
	public const string GroupChatUserAlreadyExist = "Group chat user already exist.";
	public const string GroupChatMessageNotCreated = "Group chat message not created.";
	public const string GroupChatMessageNotUpdated = "Group chat message not updated.";
	public const string GroupChatMessageNotFound = "Group chat message not found.";
	public const string ChannelNotCreated = "Channel not created.";
	public const string ChannelNotFound = "Channel not found.";
	public const string ChannelNotUpdated = "Channel not updated.";
	public const string ChannelUserAlreadyExist = "Channel user already exist.";
	public const string ChannelUserNotCreated = "Channel user not created.";
	public const string ChannelUserNotFound = "Channel user not found.";
	public const string ChannelUserNotDeleted = "Channel user not deleted.";
	public const string ChannelUserRoleAlreadySame = "Channel user role already same.";
	public const string ChannelUserRoleNotUpdated = "Channel user role not updated.";
	public const string ChannelMessageNotCreated = "Channel message not created.";
	public const string ChannelMessageNotUpdated = "Channel message not updated.";
	public const string ChannelMessageNotFound = "Channel message not found.";
	public const string AuthorNotFound = "Author not found.";
	public const string RecipientNotFound = "Recipient not found.";
	public const string PersonalMessageNotCreated = "Personal message not created.";
	public const string PersonalMessageNotFound = "Personal message not found.";
	public const string PersonalMessageNotUpdated = "Personal message not updated.";
	public const string MessageContentAlreadySame = "Message content already same.";
}
