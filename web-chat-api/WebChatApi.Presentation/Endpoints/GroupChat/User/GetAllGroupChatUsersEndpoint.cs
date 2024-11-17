using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Requests;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.GroupChat.User;

public class GetAllGroupChatUsersEndpoint : Endpoint<IdRequest, ApiResponse>
{
	private readonly IGroupChatService _groupChatService;
	public GetAllGroupChatUsersEndpoint(
		IGroupChatService groupChatService)
	{
		_groupChatService = groupChatService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("get-all");
		Group<GroupChatUserEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Get all users");
		});
		Summary(s =>
		{
			s.Summary = "Get all group chat users";
			s.Description = "Get all group chat users";
		});
	}

	public override async Task HandleAsync(IdRequest req, CancellationToken ct)
	{
		Response = await _groupChatService.GetAllGroupChatUsersByGroupChatIdAsync(req.Id);
	}
}