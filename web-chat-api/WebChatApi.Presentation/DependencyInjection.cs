using Microsoft.EntityFrameworkCore;
using WebChatApi.Infrastructure.Database;
using WebChatApi.Infrastructure.Extensions;
using Mapster;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Infrastructure.Services;
using WebChatApi.Application.Services;

namespace WebChatApi.Presentation;

public static class DependencyInjection
{
	public static async Task<IServiceProvider> PrepareDbAsync(
	this IServiceProvider services,
	bool migrateDatabase = true,
	bool initTestData = true)
	{
		await using var scope = services.CreateAsyncScope();
		await using var context = scope.ServiceProvider.GetRequiredService<WebChatApiInternalContext>();
		if (migrateDatabase)
		{
			await context.Database.MigrateAsync();
		}

		//if (!initTestData)
		//{
		//	await DefaultDbSeeder.SeedDataAsync(context);
		//}

		//if (initTestData)
		//{
		//	await context.SeedDataAsync();
		//}

		return services;
	}

	public static async Task PrepareDbAsync(this WebApplication webApplication)
	{
		await webApplication.Services.PrepareDbAsync(
			webApplication.Configuration.GetValue<bool>("Database:MigrateDatabase"),
			webApplication.Environment.IsTesting());
	}

	public static void AddServices(this IServiceCollection services)
	{
		services.AddScoped<IUserService, UserService>();
	}

	public static void ConfigureMapsterProfiles(this IServiceCollection services)
	{
		TypeAdapterConfig<CreateUserDto, UserDbo>
			.NewConfig()
			.Map(dest => dest.Username, src => src.Username)
			.Map(dest => dest.FirstName, src => src.FirstName)
			.Map(dest => dest.LastName, src => src.LastName)
			.Map(dest => dest.City, src => src.City)
			.Map(dest => dest.Description, src => src.Description)
			.Map(dest => dest.Email, src => src.Email)
			.Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
			.Map(dest => dest.GitHubUrl, src => src.GitHubUrl)
			.Map(dest => dest.LinkedInUrl, src => src.LinkedInUrl)
			.Map(dest => dest.SubscriptionAmount, src => 0)
			.Map(dest => dest.IsActive, src => true)
			.Map(dest => dest.Stack, src => new List<string>())
			.Map(dest => dest.CurrentStatus, src => "User")
			.Map(dest => dest.LastOnline, src => DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc));
	}
}
