using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Responses;

namespace WebChatApi.Application.Services.EntityServices;

public interface IUserService : IBaseService<UserDto>
{
    Task<ApiResponse> CreateUserAsync(CreateUserDto createUserDto);
    Task<ApiResponse> UpdateUserAsync(UpdateUserDto updateUserDto);
}
