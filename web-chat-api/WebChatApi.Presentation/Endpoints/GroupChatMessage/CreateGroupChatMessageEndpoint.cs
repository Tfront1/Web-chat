using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.GroupChatMessage;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.GroupChatMessage;

public class CreateGroupChatMessageEndpoint : Endpoint<CreateGroupChatMessageDto, ApiResponse>
{
	private readonly IGroupChatMessageService _groupChatMessageService;
	public CreateGroupChatMessageEndpoint(
		IGroupChatMessageService groupChatMessageService)
	{
		_groupChatMessageService = groupChatMessageService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("create");
		Group<GroupChatMessageEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Create");
		});
		Summary(s =>
		{
			s.Summary = "Create group chat message";
			s.Description = "Create group chat message";
		});
	}

	public override async Task HandleAsync(CreateGroupChatMessageDto req, CancellationToken ct)
	{
		Response = await _groupChatMessageService.CreateGroupChatMessageAsync(req);
	}
}