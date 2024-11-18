using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.ChannelMessage;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.ChannelMessage;

public class GetAllChannelMessagesByChannelEndpoint : Endpoint<GetChannelMessagesByChannelDto, ApiResponse>
{
	private readonly IChannelMessageService _channelMessageService;
	public GetAllChannelMessagesByChannelEndpoint(
		IChannelMessageService channelMessageService)
	{
		_channelMessageService = channelMessageService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("get-all-by-channel");
		Group<ChannelMessageEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("GetAll by channel");
		});
		Summary(s =>
		{
			s.Summary = "Get all channel messages by channel";
			s.Description = "Get all channel messages by channel";
		});
	}

	public override async Task HandleAsync(GetChannelMessagesByChannelDto req, CancellationToken ct)
	{
		Response = await _channelMessageService.GetAllChannelMessagesByChannelAsync(req);
	}
}