using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Requests;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.Channel;

public class GetChannelEndpoint : Endpoint<IdRequest, ApiResponse>
{
	private readonly IChannelService _channelService;
	public GetChannelEndpoint(
		IChannelService channelService)
	{
		_channelService = channelService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("get");
		Group<ChannelEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Get");
		});
		Summary(s =>
		{
			s.Summary = "Get channel";
			s.Description = "Get channel";
		});
	}

	public override async Task HandleAsync(IdRequest req, CancellationToken ct)
	{
		Response = await _channelService.GetByIdAsync(req.Id);
	}
}