using FastEndpoints;
using WebChatApi.Application.Services;
using WebChatApi.Contracts.Dtos;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.User;

public class GetUserEndpoint : Endpoint<IdRequest, ApiResponse>
{
	private readonly IUserService _userService;
	public GetUserEndpoint(IUserService userService)
	{
		_userService = userService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("get");
		Group<UserEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Get");
		});
		Summary(s =>
		{
			s.Summary = "Get user";
			s.Description = "Get user";
		});
	}

	public override async Task HandleAsync(IdRequest req, CancellationToken ct)
	{
		Response = await _userService.GetUserAsync(req.Id);
	}
}