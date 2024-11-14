using WebChatApi.Contracts.Dtos.GroupChat;
using WebChatApi.Contracts.Responses;

namespace WebChatApi.Application.Services.EntityServices;

public interface IGroupChatService : IBaseService<GroupChatDto>
{
	Task<ApiResponse> CreateGroupChatAsync(CreateGroupChatDto createGroupChatDto);
	Task<ApiResponse> UpdateGroupChatAsync(UpdateGroupChatDto updateGroupChatDto);
}