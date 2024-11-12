using Mapster;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Application.Services;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Infrastructure.Database;

namespace WebChatApi.Infrastructure.Services;

public class UserService : IUserService
{
	public readonly WebChatApiInternalContext _context;
	public UserService(WebChatApiInternalContext context)
	{
		_context = context;
	}

	public async Task CreateUserAsync(CreateUserDto createUserDto)
	{
		var existingUser = await _context.Users
			.AnyAsync(u => u.Username == createUserDto.Username);

		if (existingUser)
		{
			throw new InvalidOperationException();
		}

		var newUser = createUserDto.Adapt<UserDbo>();

		await _context.Users.AddAsync(newUser);

		await _context.SaveChangesAsync();
	}
}
