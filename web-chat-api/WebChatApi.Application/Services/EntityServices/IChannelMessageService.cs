using WebChatApi.Contracts.Dtos.ChannelMessage;
using WebChatApi.Contracts.Responses;

namespace WebChatApi.Application.Services.EntityServices;

public interface IChannelMessageService : IBaseService<ChannelMessageDto>
{
	Task<ApiResponse> CreateChannelMessageAsync(CreateChannelMessageDto createChannelMessageDto);
	Task<ApiResponse> UpdateChannelMessageContentAsync(UpdateChannelMessageContentDto updateChannelMessageContentDto);
	Task<ApiResponse> GetAllChannelMessagesByChannelAsync(GetChannelMessagesByChannelDto getChannelMessagesByChannelDto);
}
