using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.Channel;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.Channel.User;

public class UpdateChannelUserRoleEndpoint : Endpoint<UpdateChannelUserDto, ApiResponse>
{
	private readonly IChannelService _channelService;
	public UpdateChannelUserRoleEndpoint(
		IChannelService channelService)
	{
		_channelService = channelService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("update-role");
		Group<ChannelUserEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Update user role");
		});
		Summary(s =>
		{
			s.Summary = "Update channel user role";
			s.Description = "Update channel user role";
		});
	}

	public override async Task HandleAsync(UpdateChannelUserDto req, CancellationToken ct)
	{
		Response = await _channelService.UpdateChannelUserRole(req);
	}
}