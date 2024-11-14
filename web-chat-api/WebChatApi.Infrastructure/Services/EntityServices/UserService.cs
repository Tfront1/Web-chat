using Mapster;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Models;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.Database;

namespace WebChatApi.Infrastructure.Services.EntityServices;

public class UserService : BaseService<UserDbo, UserDto>, IUserService
{
    public readonly WebChatApiInternalContext _context;
    public UserService(WebChatApiInternalContext context) : base(context)
	{
        _context = context;
    }

    public async Task<ApiResponse> CreateUserAsync(CreateUserDto createUserDto)
    {
        var existingUser = await _context.Users
            .AnyAsync(u => u.Username == createUserDto.Username || u.Email == createUserDto.Email);

        if (existingUser)
        {
            return new ApiFailureResponse(ProblemDetailsResponsesModel.UserAlreadyExists);
        }

        try
        {
            var newUser = createUserDto.Adapt<UserDbo>();
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }
        catch
        {
            return new ApiFailureResponse(ProblemDetailsResponsesModel.UserNotCreated);
        }

        return ApiSuccessResponse.Empty;
    }

    public async Task<ApiResponse> UpdateUserAsync(UpdateUserDto updateUserDto)
    {
        var user = await _context.Users.FindAsync(updateUserDto.Id);

        if (user == null)
        {
            return new ApiFailureResponse(ProblemDetailsResponsesModel.UserNotFound);
        }

        try
        {
            updateUserDto.Adapt(user);
            await _context.SaveChangesAsync();
        }
        catch
        {
            return new ApiFailureResponse(ProblemDetailsResponsesModel.UserNotUpdated);
        }

        return ApiSuccessResponse.Empty;
    }
}
