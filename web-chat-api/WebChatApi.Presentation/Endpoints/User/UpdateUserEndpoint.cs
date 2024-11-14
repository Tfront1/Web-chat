using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.User;

public class UpdateUserEndpoint : Endpoint<UpdateUserDto, ApiResponse>
{
	private readonly IUserService _userService;
	public UpdateUserEndpoint(
		IUserService userService)
	{
		_userService = userService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("update");
		Group<UserEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Update");
		});
		Summary(s =>
		{
			s.Summary = "Update user";
			s.Description = "Update user";
		});
	}

	public override async Task HandleAsync(UpdateUserDto req, CancellationToken ct)
	{
		Response = await _userService.UpdateUserAsync(req);
	}
}