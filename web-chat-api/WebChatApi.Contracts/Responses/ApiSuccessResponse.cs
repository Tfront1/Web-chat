namespace WebChatApi.Contracts.Responses;

public record ApiSuccessResponse : ApiResponse
{
	public ApiSuccessResponse()
	{
		Success = true;
	}

	public ApiSuccessResponse(object payload)
	{
		Success = true;
		Payload = payload;
	}

	public static ApiSuccessResponse Empty => new();

	public static ApiSuccessResponse With<TS>(TS payload)
	{
		return new ApiSuccessResponse { Payload = payload };
	}
}