using FastEndpoints;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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