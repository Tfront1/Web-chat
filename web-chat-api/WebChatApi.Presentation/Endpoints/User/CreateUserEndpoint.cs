using FastEndpoints;
using WebChatApi.Application.Services;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.User;

public class CreateUserEndpoint : Endpoint<CreateUserDto, ApiResponse>
{
	private readonly IUserService _userService;
	public CreateUserEndpoint(
		IUserService userService)
	{
		_userService = userService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("create");
		Group<UserEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Create");
		});
		Summary(s =>
		{
			s.Summary = "Create user";
			s.Description = "Create user";
		});
	}

	public override async Task HandleAsync(CreateUserDto req, CancellationToken ct)
	{
		Response = await _userService.CreateUserAsync(req);
	}
}