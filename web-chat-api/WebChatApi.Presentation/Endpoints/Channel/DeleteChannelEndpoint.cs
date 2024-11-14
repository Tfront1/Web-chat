using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Requests;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.Channel;

public class DeleteChannelEndpoint : Endpoint<IdRequest, ApiResponse>
{
	private readonly IChannelService _channelService;
	public DeleteChannelEndpoint(
		IChannelService channelService)
	{
		_channelService = channelService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("delete");
		Group<ChannelEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Delete");
		});
		Summary(s =>
		{
			s.Summary = "Delete channel";
			s.Description = "Delete channel";
		});
	}

	public override async Task HandleAsync(IdRequest req, CancellationToken ct)
	{
		Response = await _channelService.DeleteAsync(req.Id);
	}
}