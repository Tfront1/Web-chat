using WebChatApi.Contracts.Dtos.PersonalMessage;
using WebChatApi.Contracts.Responses;

namespace WebChatApi.Application.Services.EntityServices;

public interface IPersonalMessageService : IBaseService<PersonalMessageDto>
{
	Task<ApiResponse> CreatePersonalMessageAsync(CreatePersonalMessageDto createPersonalMessageDto);
	Task<ApiResponse> UpdatePersonalMessageContentAsync(UpdatePersonalMessageContentDto updatePersonalMessageContentDto);
	Task<ApiResponse> GetAllPersonalMessagesByUsersAsync(GetPersonalMessagesByUsersDto getPersonalMessageByUsersDto);
}
