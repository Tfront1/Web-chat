using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Requests;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.GroupChat;

public class GetGroupChatEndpoint : Endpoint<IdRequest, ApiResponse>
{
	private readonly IGroupChatService _groupChatService;
	public GetGroupChatEndpoint(
		IGroupChatService groupChatService)
	{
		_groupChatService = groupChatService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("get");
		Group<GroupChatEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Get");
		});
		Summary(s =>
		{
			s.Summary = "Get group chat";
			s.Description = "Get group chat";
		});
	}

	public override async Task HandleAsync(IdRequest req, CancellationToken ct)
	{
		Response = await _groupChatService.GetByIdAsync(req.Id);
	}
}