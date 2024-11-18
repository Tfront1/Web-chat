using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace WebChatApi.Infrastructure.EndpointSettings.Groups;

public class ChannelEndpointsGroup : Group
{
	public ChannelEndpointsGroup()
	{
		Configure(
			"channel",
			ep =>
			{
				ep.Description(
					x => x.WithTags("Channel"));
			});
	}
}