using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.Channel;

public class GetAllChannelsEndpoint : Endpoint<EmptyRequest, ApiResponse>
{
	private readonly IChannelService _channelService;
	public GetAllChannelsEndpoint(
		IChannelService channelService)
	{
		_channelService = channelService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("get-all");
		Group<ChannelEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Get all");
		});
		Summary(s =>
		{
			s.Summary = "Get all channels";
			s.Description = "Get all channels";
		});
	}

	public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
	{
		Response = await _channelService.GetAllAsync();
	}
}