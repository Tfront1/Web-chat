using Microsoft.EntityFrameworkCore;
using WebChatApi.Infrastructure.Database;
using WebChatApi.Infrastructure.Extensions;
using Mapster;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Dtos.GroupChat;
using WebChatApi.Domain.Dbos;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Infrastructure.Services.EntityServices;
using WebChatApi.Contracts.Dtos.Channel;

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
		services.AddScoped<IGroupChatService, GroupChatService>();
		services.AddScoped<IChannelService, ChannelService>();
	}

	public static void ConfigureMapsterProfiles(this IServiceCollection services)
	{
		TypeAdapterConfig<CreateUserDto, UserDbo>
			.NewConfig()
			.Map(dest => dest.IsActive, src => true)
			.Map(dest => dest.Stack, src => new List<string>())
			.Map(dest => dest.CurrentStatus, src => "User")
			.Map(dest => dest.LastOnline, src => DateTime.UtcNow);

		TypeAdapterConfig<CreateGroupChatDto, GroupChatDbo>
			.NewConfig()
			.Map(dest => dest.CreatedAt, src => DateTime.UtcNow);

		TypeAdapterConfig<CreateChannelDto, ChannelDbo>
			.NewConfig()
			.Map(dest => dest.CreatedAt, src => DateTime.UtcNow);
	}
}
