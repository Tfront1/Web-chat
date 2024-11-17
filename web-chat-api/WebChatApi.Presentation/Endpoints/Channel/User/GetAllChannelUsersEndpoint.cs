using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Requests;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.Channel.User;

public class GetAllChannelUsersEndpoint : Endpoint<IdRequest, ApiResponse>
{
	private readonly IChannelService _channelService;
	public GetAllChannelUsersEndpoint(
		IChannelService channelService)
	{
		_channelService = channelService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("get-all");
		Group<ChannelUserEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Get all users");
		});
		Summary(s =>
		{
			s.Summary = "Get all channel users";
			s.Description = "Get all channel users";
		});
	}

	public override async Task HandleAsync(IdRequest req, CancellationToken ct)
	{
		Response = await _channelService.GetAllChannelUsersByChannelIdAsync(req.Id);
	}
}