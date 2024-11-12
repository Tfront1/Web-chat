namespace WebChatApi.Contracts.Responses;

public sealed record ApiFailureResponse : ApiResponse
{
	public ApiFailureResponse()
	{
		Success = false;
	}

	public ApiFailureResponse(string errorMessage)
	{
		Success = false;
		ErrorMessage = errorMessage;
	}
}
