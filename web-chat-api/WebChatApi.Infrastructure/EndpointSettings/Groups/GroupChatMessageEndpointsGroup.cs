using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace WebChatApi.Infrastructure.EndpointSettings.Groups;

public class GroupChatMessageEndpointsGroup : Group
{
	public GroupChatMessageEndpointsGroup()
	{
		Configure(
			"group-chat-message",
			ep =>
			{
				ep.Description(
					x => x.WithTags("GroupChatMessage"));
			});
	}
}