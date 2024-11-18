using WebChatApi.Contracts.Dtos.GroupChatMessage;
using WebChatApi.Contracts.Responses;

namespace WebChatApi.Application.Services.EntityServices;

public interface IGroupChatMessageService : IBaseService<GroupChatMessageDto>
{
	Task<ApiResponse> CreateGroupChatMessageAsync(CreateGroupChatMessageDto createGroupChatMessageDto);
	Task<ApiResponse> UpdateGroupChatMessageContentAsync(UpdateGroupChatMessageContentDto updateGroupChatMessageContentDto);
	Task<ApiResponse> GetAllGroupChatMessagesByGroupAsync(GetGroupChatMessagesByGroupDto getGroupChatMessagesByGroupDto);

}
