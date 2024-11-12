namespace WebChatApi.Contracts.Responses;

public record class ApiResponse
{
	public bool Success { get; set; }
	public object Payload { get; set; }
	public string ErrorMessage { get; set; }
	public ApiResponse()
	{
		Success = true;
	}
}

