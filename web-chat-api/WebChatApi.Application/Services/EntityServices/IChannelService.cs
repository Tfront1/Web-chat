using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChatApi.Contracts.Dtos.Channel;
using WebChatApi.Contracts.Dtos.GroupChat;
using WebChatApi.Contracts.Responses;

namespace WebChatApi.Application.Services.EntityServices;

public interface IChannelService : IBaseService<ChannelDto>
{
	Task<ApiResponse> CreateChannelAsync(CreateChannelDto createChannelDto);
	Task<ApiResponse> UpdateChannelAsync(UpdateChannelDto updateChannelDto);
}