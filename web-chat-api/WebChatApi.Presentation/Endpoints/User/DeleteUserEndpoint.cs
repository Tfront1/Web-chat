using FastEndpoints;
using WebChatApi.Application.Services;
using WebChatApi.Contracts.Dtos;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.User;

public class DeleteUserEndpoint : Endpoint<IdRequest, ApiResponse>
{
	private readonly IUserService _userService;
	public DeleteUserEndpoint(
		IUserService userService)
	{
		_userService = userService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("delete");
		Group<UserEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Delete");
		});
		Summary(s =>
		{
			s.Summary = "Delete user";
			s.Description = "Delete user";
		});
	}

	public override async Task HandleAsync(IdRequest req, CancellationToken ct)
	{
		Response = await _userService.DeleteUserAsync(req.Id);
	}
}