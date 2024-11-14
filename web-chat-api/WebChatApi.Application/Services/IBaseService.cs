using WebChatApi.Contracts.Responses;

namespace WebChatApi.Application.Services;

public interface IBaseService<TDto>
{
	Task<ApiResponse> GetByIdAsync(int id);
	Task<ApiResponse> GetAllAsync();
	Task<ApiResponse> DeleteAsync(int id);
}
