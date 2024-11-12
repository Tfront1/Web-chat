using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Responses;

namespace WebChatApi.Application.Services;

public interface IUserService
{
	Task<ApiResponse> CreateUserAsync(CreateUserDto createUserDto);
	Task<ApiResponse> GetUserAsync(int userId);
	Task<ApiResponse> UpdateUserAsync(UpdateUserDto updateUserDto);
	Task<ApiResponse> DeleteUserAsync(int userId);
	Task<ApiResponse> GetAllUsersAsync();
}
