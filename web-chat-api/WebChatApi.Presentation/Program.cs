using FastEndpoints;
using FastEndpoints.Swagger;
using System.Text.Json.Serialization;
using WebChatApi.Infrastructure.Database;
using WebChatApi.Infrastructure.Extensions.Database;
using WebChatApi.Infrastructure.Hubs;

namespace WebChatApi.Presentation;

public class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		//builder.Services.ConfigureMapsterProfiles();

		builder.Services.AddControllers()
			.AddJsonOptions(options =>
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		builder.Services.AddFastEndpoints(config =>
		{
			config.IncludeAbstractValidators = true;
		})
		.SwaggerDocument(o =>
		{
			o.AutoTagPathSegmentIndex = 0;
			o.DocumentSettings = s =>
			{
				s.DocumentName = "Initial-Release";
				s.Title = "WebChatApi.Presentation";
			};
		})
		.AddAuthorization();


		//await builder.Services.AddAuth(builder.Environment, builder.Configuration);

		builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", bld =>
		{
			bld
				.AllowAnyHeader()
				.AllowAnyMethod()
				.WithExposedHeaders("Content-Disposition")
				.AllowCredentials()
				.SetIsOriginAllowed(isOriginAllowed: _ => true);
		}));

		builder.Services.AddCustomDbContextPool<WebChatApiInternalContext>(
			builder.Configuration.GetConnectionString("DefaultConnection")!);

		builder.Services.ConfigureMapsterProfiles();
		builder.Services.AddServices();
		//builder.Services.AddConfigurations(builder.Configuration);

		//builder.Services.AddSignalR();

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseDefaultExceptionHandler()
			.UseFastEndpoints(c =>
			{
				c.Endpoints.Configurator = ep =>
				{
					ep.Description(b => b.Produces<ErrorResponse>(500, "application/json"));
				};
			})
			.UseSwaggerGen();

		app.UseHttpsRedirection();

		//app.MapHub<NotificationHub>("notification-hub");

		app.UseCors("CorsPolicy");

		app.UseAuthentication();
		app.UseAuthorization();

		app.UseStaticFiles();

		app.MapControllers();

		await app.PrepareDbAsync();

		await app.RunAsync();
	}
}

