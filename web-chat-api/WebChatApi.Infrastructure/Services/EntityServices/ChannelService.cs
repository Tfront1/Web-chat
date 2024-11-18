using Mapster;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.Channel;
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

	public async Task<ApiResponse> CreateChannelUserAsync(ChannelUserDto channelUserDto)
	{
		var channelUser = await _context.ChannelUsers
			.FindAsync(channelUserDto.ChannelId, channelUserDto.UserId);

		if (channelUser != null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.ChannelUserAlreadyExist);
		}

		var channel = await _context.Channels.FindAsync(channelUserDto.ChannelId);

		if (channel == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.ChannelNotFound);
		}

		var user = await _context.Users.FindAsync(channelUserDto.UserId);

		if (user == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.UserNotFound);
		}

		try
		{
			var newChannelUser = channelUserDto.Adapt<ChannelUserDbo>();
			await _context.ChannelUsers.AddAsync(newChannelUser);
			await _context.SaveChangesAsync();
		}
		catch
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.ChannelUserNotCreated);
		}

		return ApiSuccessResponse.Empty;
	}

	public async Task<ApiResponse> DeleteChannelUserAsync(ChannelUserDto channelUserDto)
	{
		var channelUser = await _context.ChannelUsers
			.FindAsync(channelUserDto.ChannelId, channelUserDto.UserId);

		if (channelUser == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.ChannelUserNotFound);
		}

		try
		{
			_context.ChannelUsers.Remove(channelUser);
			await _context.SaveChangesAsync();
		}
		catch
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.ChannelUserNotDeleted);
		}

		return ApiSuccessResponse.Empty;
	}

	public async Task<ApiResponse> GetAllChannelUsersByChannelIdAsync(int channelId)
	{
		var channel = await _context.Channels.FindAsync(channelId);

		if (channel == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.ChannelNotFound);
		}

		var channelUsers = _context.ChannelUsers.Where(x => x.ChannelId == channelId).ToList();

		var dtos = channelUsers.Adapt<List<ChannelUserDto>>();
		return new ApiSuccessResponse<List<ChannelUserDto>>(dtos);
	}

	public async Task<ApiResponse> UpdateChannelUserRole(UpdateChannelUserDto updateChannelUserDto)
	{
		var channelUser = await _context.ChannelUsers
			.FindAsync(updateChannelUserDto.ChannelId, updateChannelUserDto.UserId);

		if (channelUser == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.ChannelUserNotFound);
		}

		if (channelUser.IsAdmin == updateChannelUserDto.IsAdmin) 
		{	
			return new ApiFailureResponse(ProblemDetailsResponsesModel.ChannelUserRoleAlreadySame);
		}

		try
		{
			channelUser.IsAdmin = updateChannelUserDto.IsAdmin;
			await _context.SaveChangesAsync();
		}
		catch
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.ChannelUserRoleNotUpdated);
		}

		return ApiSuccessResponse.Empty;
	}
}
