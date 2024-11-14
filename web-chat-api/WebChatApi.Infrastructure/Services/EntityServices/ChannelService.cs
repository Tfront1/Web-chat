using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.Channel;
using WebChatApi.Contracts.Dtos.GroupChat;
using WebChatApi.Contracts.Models;
using WebChatApi.Contracts.Responses;
using WebChatApi.Domain.Dbos;
using WebChatApi.Infrastructure.Database;

namespace WebChatApi.Infrastructure.Services.EntityServices;

public class ChannelService : BaseService<ChannelDbo, ChannelDto>, IChannelService
{
	public readonly WebChatApiInternalContext _context;
	public ChannelService(WebChatApiInternalContext context) : base(context)
	{
		_context = context;
	}

	public async Task<ApiResponse> CreateChannelAsync(CreateChannelDto createChannelDto)
	{
		var existingUser = await _context.Users.AnyAsync(u => u.Id == createChannelDto.CreatorId);

		if (!existingUser)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.UserNotFound);
		}

		try
		{
			var newChannel = createChannelDto.Adapt<ChannelDbo>();
			await _context.Channels.AddAsync(newChannel);
			await _context.SaveChangesAsync();
		}
		catch
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.ChannelNotCreated);
		}

		return ApiSuccessResponse.Empty;
	}

	public async Task<ApiResponse> UpdateChannelAsync(UpdateChannelDto updateChannelDto)
	{
		var channel = await _context.Channels.FindAsync(updateChannelDto.Id);

		if (channel == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.ChannelNotFound);
		}

		try
		{
			updateChannelDto.Adapt(channel);
			await _context.SaveChangesAsync();
		}
		catch
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.ChannelNotUpdated);
		}

		return ApiSuccessResponse.Empty;
	}
}
