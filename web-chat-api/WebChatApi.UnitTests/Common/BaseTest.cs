using WebChatApi.Infrastructure.Database;

namespace WebChatApi.UnitTests.Common;

public abstract class BaseTest : IDisposable
{
	protected readonly WebChatApiInternalContext _context;

	public BaseTest()
	{
		_context = ContextFactory.Create();
	}
	public void Dispose()
	{
		ContextFactory.Destroy(_context);
	}
}
