using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.Channel;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.Channel.User;

public class CreateChannelUserEndpoint : Endpoint<ChannelUserDto, ApiResponse>
{
	private readonly IChannelService _channelService;
	public CreateChannelUserEndpoint(
		IChannelService channelService)
	{
		_channelService = channelService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("create");
		Group<ChannelUserEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Create user");
		});
		Summary(s =>
		{
			s.Summary = "Create channel user";
			s.Description = "Create channel user";
		});
	}

	public override async Task HandleAsync(ChannelUserDto req, CancellationToken ct)
	{
		Response = await _channelService.CreateChannelUserAsync(req);
	}
}