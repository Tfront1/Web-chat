using FastEndpoints;
using WebChatApi.Application.Services;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Infrastructure.EndpointSettings.Groups;
using WebChatApi.Infrastructure.Services;

namespace WebChatApi.Presentation.Endpoints.User;

public class CreateUsersEndpoint : Endpoint<CreateUserDto, EmptyResponse>
{
	private readonly IUserService _userService;
	public CreateUsersEndpoint(
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
		await _userService.CreateUserAsync(req);
	}
}