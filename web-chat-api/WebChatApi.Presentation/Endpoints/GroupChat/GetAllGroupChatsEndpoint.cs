using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.GroupChat;

public class GetAllGroupChatsEndpoint : Endpoint<EmptyRequest, ApiResponse>
{
	private readonly IGroupChatService _groupChatService;
	public GetAllGroupChatsEndpoint(
		IGroupChatService groupChatService)
	{
		_groupChatService = groupChatService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("get-all");
		Group<GroupChatEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Get all");
		});
		Summary(s =>
		{
			s.Summary = "Get all group chats";
			s.Description = "Get all group chats";
		});
	}

	public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
	{
		Response = await _groupChatService.GetAllAsync();
	}
}