using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Requests;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.GroupChatMessage;

public class GetGroupChatMessageEndpoint : Endpoint<IdRequest, ApiResponse>
{
	private readonly IGroupChatMessageService _groupChatMessageService;
	public GetGroupChatMessageEndpoint(
		IGroupChatMessageService groupChatMessageService)
	{
		_groupChatMessageService = groupChatMessageService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("get");
		Group<GroupChatMessageEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Get");
		});
		Summary(s =>
		{
			s.Summary = "Get personal message";
			s.Description = "Get personal message";
		});
	}

	public override async Task HandleAsync(IdRequest req, CancellationToken ct)
	{
		Response = await _groupChatMessageService.GetByIdAsync(req.Id);
	}
}