using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.Channel;
using WebChatApi.Contracts.Dtos.GroupChat;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.Channel;

public class CreateChannelEndpoint : Endpoint<CreateChannelDto, ApiResponse>
{
	private readonly IChannelService _channelService;
	public CreateChannelEndpoint(
		IChannelService channelService)
	{
		_channelService = channelService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("create");
		Group<ChannelEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Create");
		});
		Summary(s =>
		{
			s.Summary = "Create channel";
			s.Description = "Create channel";
		});
	}

	public override async Task HandleAsync(CreateChannelDto req, CancellationToken ct)
	{
		Response = await _channelService.CreateChannelAsync(req);
	}
}