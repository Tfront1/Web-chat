using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace WebChatApi.Infrastructure.EndpointSettings.Groups;

public class PersonalMessageEndpointsGroup : Group
{
	public PersonalMessageEndpointsGroup()
	{
		Configure(
			"personal-message",
			ep =>
			{
				ep.Description(
					x => x.WithTags("PersonalMessage"));
			});
	}
}