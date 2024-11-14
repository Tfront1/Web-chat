using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.GroupChat;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.GroupChat;

public class CreateGroupChatEndpoint : Endpoint<CreateGroupChatDto, ApiResponse>
{
	private readonly IGroupChatService _groupChatService;
	public CreateGroupChatEndpoint(
		IGroupChatService groupChatService)
	{
		_groupChatService = groupChatService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("create");
		Group<GroupChatEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Create");
		});
		Summary(s =>
		{
			s.Summary = "Create group chat";
			s.Description = "Create group chat";
		});
	}

	public override async Task HandleAsync(CreateGroupChatDto req, CancellationToken ct)
	{
		Response = await _groupChatService.CreateGroupChatAsync(req);
	}
}