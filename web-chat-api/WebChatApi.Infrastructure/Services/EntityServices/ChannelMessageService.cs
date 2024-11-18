using Mapster;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.ChannelMessage;
using WebChatApi.Contracts.Models;
using WebChatApi.Contracts.Responses;
using WebChatApi.Domain.Dbos;
using WebChatApi.Infrastructure.Database;

namespace WebChatApi.Infrastructure.Services.EntityServices;

public class ChannelMessageService : BaseService<ChannelMessageDbo, ChannelMessageDto>, IChannelMessageService
{
	public readonly WebChatApiInternalContext _context;
	public ChannelMessageService(WebChatApiInternalContext context) : base(context)
	{
		_context = context;
	}

	public async Task<ApiResponse> CreateChannelMessageAsync(CreateChannelMessageDto createChannelMessageDto)
	{
		var author = await _context.Users.FindAsync(createChannelMessageDto.AuthorId);

		if (author == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.AuthorNotFound);
		}

		var channel = await _context.Channels.FindAsync(createChannelMessageDto.ChannelId);

		if (channel == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.ChannelNotFound);
		}

		try
		{
			var newChannelMessage = createChannelMessageDto.Adapt<ChannelMessageDbo>();
			await _context.ChannelMessages.AddAsync(newChannelMessage);
			await _context.SaveChangesAsync();
		}
		catch
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.ChannelMessageNotCreated);
		}

		return ApiSuccessResponse.Empty;
	}

	public async Task<ApiResponse> UpdateChannelMessageContentAsync(UpdateChannelMessageContentDto updateChannelMessageContentDto)
	{
		var channelMessage = await _context.ChannelMessages.FindAsync(updateChannelMessageContentDto.Id);

		if (channelMessage == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.ChannelMessageNotFound);
		}

		if (channelMessage.Content == updateChannelMessageContentDto.Content)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.MessageContentAlreadySame);
		}

		try
		{

			channelMessage.Content = updateChannelMessageContentDto.Content;
			channelMessage.IsEdited = true;
			channelMessage.EditedAt = DateTime.UtcNow;
			await _context.SaveChangesAsync();
		}
		catch
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.ChannelMessageNotUpdated);
		}

		return ApiSuccessResponse.Empty;
	}

	public async Task<ApiResponse> GetAllChannelMessagesByChannelAsync(GetChannelMessagesByChannelDto getChannelMessagesByChannelDto)
	{
		var channel = await _context.Channels.FindAsync(getChannelMessagesByChannelDto.ChannelId);

		if (channel == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.ChannelNotFound);
		}

		var channelMessages = await _context.ChannelMessages
			.Where(x => x.ChannelId == getChannelMessagesByChannelDto.ChannelId).ToListAsync();

		if (channelMessages == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.ChannelMessageNotFound);
		}

		var dtos = channelMessages.Adapt<List<ChannelMessageDto>>();
		return ApiSuccessResponse.With(dtos);
	}
}
