using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.ChannelMessage;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.ChannelMessage;

public class UpdateChannelMessageContentEndpoint : Endpoint<UpdateChannelMessageContentDto, ApiResponse>
{
	private readonly IChannelMessageService _channelMessageService;
	public UpdateChannelMessageContentEndpoint(
		IChannelMessageService channelMessageService)
	{
		_channelMessageService = channelMessageService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("update-content");
		Group<ChannelMessageEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Update content");
		});
		Summary(s =>
		{
			s.Summary = "Update channel message content";
			s.Description = "Update channel message content";
		});
	}

	public override async Task HandleAsync(UpdateChannelMessageContentDto req, CancellationToken ct)
	{
		Response = await _channelMessageService.UpdateChannelMessageContentAsync(req);
	}
}