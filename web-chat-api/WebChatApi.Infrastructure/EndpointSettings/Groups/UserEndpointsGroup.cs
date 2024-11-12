using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace WebChatApi.Infrastructure.EndpointSettings.Groups;

public class UserEndpointsGroup : Group
{
	public UserEndpointsGroup()
	{
		Configure(
			"user",
			ep =>
			{
				ep.Description(
					x => x.WithTags("User"));
			});
	}
}