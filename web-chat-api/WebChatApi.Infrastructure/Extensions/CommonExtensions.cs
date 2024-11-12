using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WebChatApi.Infrastructure.Extensions;

public static class CommonExtensions
{
	public static bool IsTesting(this IWebHostEnvironment environment)
	{
		return environment.IsEnvironment("Testing");
	}
}
