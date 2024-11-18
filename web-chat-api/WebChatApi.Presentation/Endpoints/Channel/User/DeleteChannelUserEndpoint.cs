using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.Channel;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.Channel.User;

public class DeleteChannelUserEndpoint : Endpoint<ChannelUserDto, ApiResponse>
{
	private readonly IChannelService _channelService;
	public DeleteChannelUserEndpoint(
		IChannelService channelService)
	{
		_channelService = channelService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("delete");
		Group<ChannelUserEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Delete user");
		});
		Summary(s =>
		{
			s.Summary = "Delete channel user";
			s.Description = "Delete channel user";
		});
	}

	public override async Task HandleAsync(ChannelUserDto req, CancellationToken ct)
	{
		Response = await _channelService.DeleteChannelUserAsync(req);
	}
}