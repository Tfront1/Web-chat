using FastEndpoints;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChatApi.Infrastructure.EndpointSettings.Groups;

public class ChannelUserEndpointsGroup : Group
{
	public ChannelUserEndpointsGroup()
	{
		Configure(
			"channel/user",
			ep =>
			{
				ep.Description(
					x => x.WithTags("ChannelUser"));
			});
	}
}