using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.ChannelMessage;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.ChannelMessage;

public class CreateChannelMessageEndpoint : Endpoint<CreateChannelMessageDto, ApiResponse>
{
	private readonly IChannelMessageService _channelMessageService;
	public CreateChannelMessageEndpoint(
		IChannelMessageService channelMessageService)
	{
		_channelMessageService = channelMessageService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("create");
		Group<ChannelMessageEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Create");
		});
		Summary(s =>
		{
			s.Summary = "Create channel message";
			s.Description = "Create channel message";
		});
	}

	public override async Task HandleAsync(CreateChannelMessageDto req, CancellationToken ct)
	{
		Response = await _channelMessageService.CreateChannelMessageAsync(req);
	}
}