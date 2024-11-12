using WebChatApi.Contracts.Dtos.User;

namespace WebChatApi.Application.Services;

public interface IUserService
{
	Task CreateUserAsync(CreateUserDto createUserDto);
}
