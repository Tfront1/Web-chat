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
}
