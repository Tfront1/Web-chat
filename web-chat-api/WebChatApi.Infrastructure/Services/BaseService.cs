using Mapster;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Application.Services;
using WebChatApi.Contracts.Models;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.Database;

namespace WebChatApi.Infrastructure.Services;

public abstract class BaseService<TEntity, TDto> : IBaseService<TDto> where TEntity : class
{
	protected readonly WebChatApiInternalContext _context;

	protected BaseService(WebChatApiInternalContext context)
	{
		_context = context;
	}

	public async Task<ApiResponse> GetByIdAsync(int id)
	{
		var entity = await _context.Set<TEntity>().FindAsync(id);

		if (entity == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.EntityNotFound);
		}

		var dto = entity.Adapt<TDto>();
		return new ApiSuccessResponse<TDto>(dto);
	}

	public async Task<ApiResponse> GetAllAsync()
	{
		var entities = await _context.Set<TEntity>().ToListAsync();
		var dtos = entities.Adapt<List<TDto>>();
		return new ApiSuccessResponse<List<TDto>>(dtos);
	}

	public async Task<ApiResponse> DeleteAsync(int id)
	{
		var entity = await _context.Set<TEntity>().FindAsync(id);

		if (entity == null)
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.EntityNotFound);
		}

		try
		{
			_context.Set<TEntity>().Remove(entity);
			await _context.SaveChangesAsync();
		}
		catch
		{
			return new ApiFailureResponse(ProblemDetailsResponsesModel.EntityDeleteFailed);
		}

		return ApiSuccessResponse.Empty;
	}
}