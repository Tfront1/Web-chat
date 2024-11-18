using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Requests;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.ChannelMessage;

public class GetChannelMessageEndpoint : Endpoint<IdRequest, ApiResponse>
{
	private readonly IChannelMessageService _channelMessageService;
	public GetChannelMessageEndpoint(
		IChannelMessageService channelMessageService)
	{
		_channelMessageService = channelMessageService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("get");
		Group<ChannelMessageEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Get");
		});
		Summary(s =>
		{
			s.Summary = "Get channel message";
			s.Description = "Get channel message";
		});
	}

	public override async Task HandleAsync(IdRequest req, CancellationToken ct)
	{
		Response = await _channelMessageService.GetByIdAsync(req.Id);
	}
}