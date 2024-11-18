using Mapster;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.PersonalMessage;
using WebChatApi.Contracts.Models;
using WebChatApi.Contracts.Responses;
using WebChatApi.Domain.Dbos;
using WebChatApi.Infrastructure.Database;

namespace WebChatApi.Infrastructure.Services.EntityServices;

public class PersonalMessageService : BaseService<PersonalMessageDbo, PersonalMessageDto>, IPersonalMessageService
{
	public readonly WebChatApiInternalContext _context;
	public PersonalMessageService(WebChatApiInternalContext context) : base(context)
	{
		_context = context;
	}

	public async Task<ApiResponse> CreatePersonalMessageAsync(CreatePersonalMessageDto createPersonalMessageDto)
	{
		var author = await _context.Users.FindAsync(createPersonalMessageDto.AuthorId);

		if (author == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.AuthorNotFound);
		}

		var recipient = await _context.Users.FindAsync(createPersonalMessageDto.RecipientId);

		if (recipient == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.RecipientNotFound);
		}

		try
		{
			var newPersonalMessage = createPersonalMessageDto.Adapt<PersonalMessageDbo>();
			await _context.PersonalMessages.AddAsync(newPersonalMessage);
			await _context.SaveChangesAsync();
		}
		catch
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.PersonalMessageNotCreated);
		}

		return ApiSuccessResponse.Empty;
	}

	public async Task<ApiResponse> UpdatePersonalMessageContentAsync(UpdatePersonalMessageContentDto updatePersonalMessageContentDto)
	{
		var personalMessage = await _context.PersonalMessages.FindAsync(updatePersonalMessageContentDto.Id);

		if (personalMessage == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.PersonalMessageNotFound);
		}

		if (personalMessage.Content == updatePersonalMessageContentDto.Content) 
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.MessageContentAlreadySame);
		}

		try
		{

			personalMessage.Content = updatePersonalMessageContentDto.Content;
			personalMessage.IsEdited = true;
			personalMessage.EditedAt = DateTime.UtcNow;
			await _context.SaveChangesAsync();
		}
		catch
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.PersonalMessageNotUpdated);
		}

		return ApiSuccessResponse.Empty;
	}

	public async Task<ApiResponse> GetAllPersonalMessagesByUsersAsync(GetPersonalMessagesByUsersDto getPersonalMessageByUsersDto)
	{
		var author = await _context.Users.FindAsync(getPersonalMessageByUsersDto.AuthorId);

		if (author == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.AuthorNotFound);
		}

		var recipient = await _context.Users.FindAsync(getPersonalMessageByUsersDto.RecipientId);

		if (recipient == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.RecipientNotFound);
		}

		var personalMessages = await _context.PersonalMessages
			.Where(x => x.AuthorId == getPersonalMessageByUsersDto.AuthorId &&
			x.RecipientId == getPersonalMessageByUsersDto.RecipientId).ToListAsync();

		if (personalMessages == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.PersonalMessageNotFound);
		}

		var dtos = personalMessages.Adapt<List<PersonalMessageDto>>();
		return ApiSuccessResponse.With(dtos);
	}
}
