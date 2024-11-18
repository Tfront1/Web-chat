using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.GroupChatMessage;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.GroupChatMessage;

public class UpdateGroupChatMessageContentEndpoint : Endpoint<UpdateGroupChatMessageContentDto, ApiResponse>
{
	private readonly IGroupChatMessageService _groupChatMessageService;
	public UpdateGroupChatMessageContentEndpoint(
		IGroupChatMessageService groupChatMessageService)
	{
		_groupChatMessageService = groupChatMessageService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("update-content");
		Group<GroupChatMessageEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Update content");
		});
		Summary(s =>
		{
			s.Summary = "Update group chat message content";
			s.Description = "Update group chat message content";
		});
	}

	public override async Task HandleAsync(UpdateGroupChatMessageContentDto req, CancellationToken ct)
	{
		Response = await _groupChatMessageService.UpdateGroupChatMessageContentAsync(req);
	}
}