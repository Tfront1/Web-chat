using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace WebChatApi.Infrastructure.EndpointSettings.Groups;

public class GroupChatUserEndpointsGroup : Group
{
	public GroupChatUserEndpointsGroup()
	{
		Configure(
			"group-chat/user",
			ep =>
			{
				ep.Description(
					x => x.WithTags("GroupChatUser"));
			});
	}
}