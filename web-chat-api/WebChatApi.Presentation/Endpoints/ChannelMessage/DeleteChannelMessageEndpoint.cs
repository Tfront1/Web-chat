using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Requests;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.ChannelMessage;

public class DeleteChannelMessageEndpoint : Endpoint<IdRequest, ApiResponse>
{
	private readonly IChannelMessageService _channelMessageService;
	public DeleteChannelMessageEndpoint(
		IChannelMessageService channelMessageService)
	{
		_channelMessageService = channelMessageService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("delete");
		Group<ChannelMessageEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Delete");
		});
		Summary(s =>
		{
			s.Summary = "Delete channel message";
			s.Description = "Delete channel message";
		});
	}

	public override async Task HandleAsync(IdRequest req, CancellationToken ct)
	{
		Response = await _channelMessageService.DeleteAsync(req.Id);
	}
}