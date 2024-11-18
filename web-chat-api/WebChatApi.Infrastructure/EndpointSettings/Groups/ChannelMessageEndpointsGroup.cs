using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace WebChatApi.Infrastructure.EndpointSettings.Groups;


public class ChannelMessageEndpointsGroup : Group
{
	public ChannelMessageEndpointsGroup()
	{
		Configure(
			"Channel-message",
			ep =>
			{
				ep.Description(
					x => x.WithTags("ChannelMessage"));
			});
	}
}