using Microsoft.EntityFrameworkCore;
using WebChatApi.Infrastructure.Database;

namespace WebChatApi.UnitTests.Common;

public class ContextFactory
{
	public static WebChatApiInternalContext Create() 
	{
		var options = new DbContextOptionsBuilder<WebChatApiInternalContext>()
			.UseInMemoryDatabase(Guid.NewGuid().ToString())
			.Options;

		var context = new WebChatApiInternalContext(options);
		context.Database.EnsureCreated();

		return context;
	}

	public static void Destroy(WebChatApiInternalContext context) 
	{
		context.Database.EnsureDeleted();
		context.Dispose();
	}
}
