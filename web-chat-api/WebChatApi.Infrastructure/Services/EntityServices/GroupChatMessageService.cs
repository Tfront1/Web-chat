using Mapster;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.GroupChatMessage;
using WebChatApi.Contracts.Models;
using WebChatApi.Contracts.Responses;
using WebChatApi.Domain.Dbos;
using WebChatApi.Infrastructure.Database;

namespace WebChatApi.Infrastructure.Services.EntityServices;

public class GroupChatMessageService : BaseService<GroupChatMessageDbo, GroupChatMessageDto>, IGroupChatMessageService
{
	public readonly WebChatApiInternalContext _context;
	public GroupChatMessageService(WebChatApiInternalContext context) : base(context)
	{
		_context = context;
	}

	public async Task<ApiResponse> CreateGroupChatMessageAsync(CreateGroupChatMessageDto createGroupChatMessageDto)
	{
		var author = await _context.Users.FindAsync(createGroupChatMessageDto.AuthorId);

		if (author == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.AuthorNotFound);
		}

		var groupChat = await _context.GroupChats.FindAsync(createGroupChatMessageDto.GroupChatId);

		if (groupChat == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.GroupChatNotFound);
		}

		try
		{
			var newGroupChatMessage = createGroupChatMessageDto.Adapt<GroupChatMessageDbo>();
			await _context.GroupChatMessages.AddAsync(newGroupChatMessage);
			await _context.SaveChangesAsync();
		}
		catch
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.GroupChatMessageNotCreated);
		}

		return ApiSuccessResponse.Empty;
	}

	public async Task<ApiResponse> UpdateGroupChatMessageContentAsync(UpdateGroupChatMessageContentDto updateGroupChatMessageContentDto)
	{
		var groupChatMessage = await _context.GroupChatMessages.FindAsync(updateGroupChatMessageContentDto.Id);

		if (groupChatMessage == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.GroupChatMessageNotFound);
		}

		if (groupChatMessage.Content == updateGroupChatMessageContentDto.Content)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.MessageContentAlreadySame);
		}

		try
		{

			groupChatMessage.Content = updateGroupChatMessageContentDto.Content;
			groupChatMessage.IsEdited = true;
			groupChatMessage.EditedAt = DateTime.UtcNow;
			await _context.SaveChangesAsync();
		}
		catch
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.GroupChatMessageNotUpdated);
		}

		return ApiSuccessResponse.Empty;
	}

	public async Task<ApiResponse> GetAllGroupChatMessagesByGroupAsync(GetGroupChatMessagesByGroupDto getGroupChatMessagesByGroupDto)
	{
		var groupChat = await _context.GroupChats.FindAsync(getGroupChatMessagesByGroupDto.GroupChatId);

		if (groupChat == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.GroupChatNotFound);
		}

		var groupChatMessages = await _context.GroupChatMessages
			.Where(x => x.GroupChatId == getGroupChatMessagesByGroupDto.GroupChatId).ToListAsync();

		if (groupChatMessages == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.GroupChatMessageNotFound);
		}

		var dtos = groupChatMessages.Adapt<List<GroupChatMessageDto>>();
		return ApiSuccessResponse.With(dtos);
	}
}
