using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.GroupChatMessage;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.GroupChatMessage;

public class GetAllGroupChatMessagesByGroupEndpoint : Endpoint<GetGroupChatMessagesByGroupDto, ApiResponse>
{
	private readonly IGroupChatMessageService _groupChatMessageService;
	public GetAllGroupChatMessagesByGroupEndpoint(
		IGroupChatMessageService groupChatMessageService)
	{
		_groupChatMessageService = groupChatMessageService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("get-all-by-group");
		Group<GroupChatMessageEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("GetAll by group");
		});
		Summary(s =>
		{
			s.Summary = "Get all group chat messages by group chat";
			s.Description = "Get all group chat messages by group chat";
		});
	}

	public override async Task HandleAsync(GetGroupChatMessagesByGroupDto req, CancellationToken ct)
	{
		Response = await _groupChatMessageService.GetAllGroupChatMessagesByGroupAsync(req);
	}
}