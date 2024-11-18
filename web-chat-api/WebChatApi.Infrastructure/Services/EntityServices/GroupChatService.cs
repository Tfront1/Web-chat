using Mapster;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.GroupChat;
using WebChatApi.Contracts.Models;
using WebChatApi.Contracts.Responses;
using WebChatApi.Domain.Dbos;
using WebChatApi.Infrastructure.Database;

namespace WebChatApi.Infrastructure.Services.EntityServices;

public class GroupChatService : BaseService<GroupChatDbo, GroupChatDto>, IGroupChatService
{
    public readonly WebChatApiInternalContext _context;
    public GroupChatService(WebChatApiInternalContext context) : base(context)
	{
        _context = context;
    }

    public async Task<ApiResponse> CreateGroupChatAsync(CreateGroupChatDto createGroupChatDto)
    {
        var existingUser = await _context.Users.AnyAsync(u => u.Id == createGroupChatDto.CreatorId);

        if (!existingUser)
        {
            return new ApiFailureResponse(ProblemDetailsResponsesModel.UserNotFound);
        }

        try
        {
            var newGroupChat = createGroupChatDto.Adapt<GroupChatDbo>();
            await _context.GroupChats.AddAsync(newGroupChat);
            await _context.SaveChangesAsync();
        }
        catch
        {
            return new ApiFailureResponse(ProblemDetailsResponsesModel.GroupChatNotCreated);
        }

        return ApiSuccessResponse.Empty;
    }

    public async Task<ApiResponse> UpdateGroupChatAsync(UpdateGroupChatDto updateGroupChatDto)
    {
        var groupChat = await _context.GroupChats.FindAsync(updateGroupChatDto.Id);

        if (groupChat == null)
        {
            return new ApiFailureResponse(ProblemDetailsResponsesModel.GroupChatNotFound);
        }

        try
        {
            updateGroupChatDto.Adapt(groupChat);
            await _context.SaveChangesAsync();
        }
        catch
        {
            return new ApiFailureResponse(ProblemDetailsResponsesModel.GroupChatNotUpdated);
        }

        return ApiSuccessResponse.Empty;
    }

	public async Task<ApiResponse> CreateGroupChatUserAsync(GroupChatUserDto groupChatUserDto)
	{
		var groupChatUser = await _context.GroupChatUsers
			.FindAsync(groupChatUserDto.GroupChatId, groupChatUserDto.UserId);

		if (groupChatUser != null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.GroupChatUserAlreadyExist);
		}

		var groupChat = await _context.GroupChats.FindAsync(groupChatUserDto.GroupChatId);

		if (groupChat == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.GroupChatNotFound);
		}

		var user = await _context.Users.FindAsync(groupChatUserDto.UserId);

		if (user == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.UserNotFound);
		}

		try
		{
			var newGroupChatUser = groupChatUserDto.Adapt<GroupChatUserDbo>();
			await _context.GroupChatUsers.AddAsync(newGroupChatUser);
			await _context.SaveChangesAsync();
		}
		catch
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.GroupChatUserNotCreated);
		}

		return ApiSuccessResponse.Empty;
	}

	public async Task<ApiResponse> DeleteGroupChatUserAsync(GroupChatUserDto groupChatUserDto)
	{
		var groupChatUser = await _context.GroupChatUsers
			.FindAsync(groupChatUserDto.GroupChatId, groupChatUserDto.UserId);

		if (groupChatUser == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.GroupChatUserNotFound);
		}

		try
		{
			_context.GroupChatUsers.Remove(groupChatUser);
			await _context.SaveChangesAsync();
		}
		catch
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.GroupChatUserNotDeleted);
		}

		return ApiSuccessResponse.Empty;
	}

	public async Task<ApiResponse> GetAllGroupChatUsersByGroupChatIdAsync(int groupChatId)
	{
		var groupChat = await _context.GroupChats.FindAsync(groupChatId);

		if (groupChat == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.GroupChatNotFound);
		}

		var groupChatUsers = _context.GroupChatUsers.Where(x => x.GroupChatId == groupChatId).ToList();

		var dtos = groupChatUsers.Adapt<List<GroupChatUserDto>>();
		return ApiSuccessResponse.With(dtos);
	}
}
