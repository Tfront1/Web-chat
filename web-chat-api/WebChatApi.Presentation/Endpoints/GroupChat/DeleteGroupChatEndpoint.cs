using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Requests;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.GroupChat;

public class DeleteGroupChatEndpoint : Endpoint<IdRequest, ApiResponse>
{
	private readonly IGroupChatService _groupChatService;
	public DeleteGroupChatEndpoint(
		IGroupChatService groupChatService)
	{
		_groupChatService = groupChatService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("delete");
		Group<GroupChatEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Delete");
		});
		Summary(s =>
		{
			s.Summary = "Delete group chat";
			s.Description = "Delete group chat";
		});
	}

	public override async Task HandleAsync(IdRequest req, CancellationToken ct)
	{
		Response = await _groupChatService.DeleteAsync(req.Id);
	}
}