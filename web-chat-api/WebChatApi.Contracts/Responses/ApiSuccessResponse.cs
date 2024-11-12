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
		return new ApiSuccessResponse<TS>(payload);
	}
}

public record ApiSuccessResponse<T> : ApiSuccessResponse
{
	public ApiSuccessResponse(T payload)
	{
		Success = true;
		Payload = payload;
	}

	public new T Payload { get; set; }
}