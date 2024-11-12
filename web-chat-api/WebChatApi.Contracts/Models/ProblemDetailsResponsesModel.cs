namespace WebChatApi.Contracts.Models;

public static class ProblemDetailsResponsesModel
{
	public static readonly List<string> ProblemDetailsResponses =
	[
		UserAlreadyExists,
		UserNotCreated,
		UserNotFound,
		UserNotDeleted,
		UserNotUpdated,
	];

	public const string UserAlreadyExists = "User already exists.";
	public const string UserNotCreated = "User not created.";
	public const string UserNotFound = "User not found.";
	public const string UserNotDeleted = "User not deleted.";
	public const string UserNotUpdated = "User not updated.";
}

