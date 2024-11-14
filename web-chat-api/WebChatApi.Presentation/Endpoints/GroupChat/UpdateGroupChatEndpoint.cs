using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.GroupChat;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.GroupChat;

public class UpdateGroupChatEndpoint : Endpoint<UpdateGroupChatDto, ApiResponse>
{
	private readonly IGroupChatService _groupChatService;
	public UpdateGroupChatEndpoint(
		IGroupChatService groupChatService)
	{
		_groupChatService = groupChatService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("update");
		Group<GroupChatEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Update");
		});
		Summary(s =>
		{
			s.Summary = "Update group chat";
			s.Description = "Update group chat";
		});
	}

	public override async Task HandleAsync(UpdateGroupChatDto req, CancellationToken ct)
	{
		Response = await _groupChatService.UpdateGroupChatAsync(req);
	}
}