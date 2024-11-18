using WebChatApi.Contracts.Dtos.Channel;
using WebChatApi.Contracts.Responses;

namespace WebChatApi.Application.Services.EntityServices;

public interface IChannelService : IBaseService<ChannelDto>
{
	Task<ApiResponse> CreateChannelAsync(CreateChannelDto createChannelDto);
	Task<ApiResponse> UpdateChannelAsync(UpdateChannelDto updateChannelDto);
	Task<ApiResponse> CreateChannelUserAsync(ChannelUserDto channelUserDto);
	Task<ApiResponse> DeleteChannelUserAsync(ChannelUserDto channelUserDto);
	Task<ApiResponse> GetAllChannelUsersByChannelIdAsync(int channelId);
	Task<ApiResponse> UpdateChannelUserRole(UpdateChannelUserDto updateChannelUserDto);
}