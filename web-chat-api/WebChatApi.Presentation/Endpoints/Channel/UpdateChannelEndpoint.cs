using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.Channel;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.Channel;

public class UpdateChannelEndpoint : Endpoint<UpdateChannelDto, ApiResponse>
{
	private readonly IChannelService _channelService;
	public UpdateChannelEndpoint(
		IChannelService channelService)
	{
		_channelService = channelService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("update");
		Group<ChannelEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Update");
		});
		Summary(s =>
		{
			s.Summary = "Update channel";
			s.Description = "Update channel";
		});
	}

	public override async Task HandleAsync(UpdateChannelDto req, CancellationToken ct)
	{
		Response = await _channelService.UpdateChannelAsync(req);
	}
}