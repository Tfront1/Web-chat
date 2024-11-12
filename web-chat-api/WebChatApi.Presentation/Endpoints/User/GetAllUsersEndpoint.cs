using FastEndpoints;
using WebChatApi.Application.Services;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.User;

public class GetAllUsersEndpoint : Endpoint<EmptyRequest, ApiResponse>
{
	private readonly IUserService _userService;
	public GetAllUsersEndpoint(
		IUserService userService)
	{
		_userService = userService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("get-all");
		Group<UserEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Get all");
		});
		Summary(s =>
		{
			s.Summary = "Get all users";
			s.Description = "Get all user";
		});
	}

	public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
	{
		Response = await _userService.GetAllUsersAsync();
	}
}