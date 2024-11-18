using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Requests;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.GroupChatMessage;

public class DeleteGroupChatMessageEndpoint : Endpoint<IdRequest, ApiResponse>
{
	private readonly IGroupChatMessageService _groupChatMessageService;
	public DeleteGroupChatMessageEndpoint(
		IGroupChatMessageService groupChatMessageService)
	{
		_groupChatMessageService = groupChatMessageService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("delete");
		Group<GroupChatMessageEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Delete");
		});
		Summary(s =>
		{
			s.Summary = "Delete group chat message";
			s.Description = "Delete group chat message";
		});
	}

	public override async Task HandleAsync(IdRequest req, CancellationToken ct)
	{
		Response = await _groupChatMessageService.DeleteAsync(req.Id);
	}
}