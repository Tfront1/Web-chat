using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace WebChatApi.Infrastructure.EndpointSettings.Groups;

public class GroupChatEndpointsGroup : Group
{
	public GroupChatEndpointsGroup()
	{
		Configure(
			"group-chat",
			ep =>
			{
				ep.Description(
					x => x.WithTags("GroupChat"));
			});
	}
}