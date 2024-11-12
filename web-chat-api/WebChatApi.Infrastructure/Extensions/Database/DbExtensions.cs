using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace WebChatApi.Infrastructure.Extensions.Database;

public static class DbExtensions
{
	public static IServiceCollection AddCustomDbContextPool<TContext>(
		this IServiceCollection services,
		string connectionString,
		string? migrationAssembly = default)
		where TContext : DbContext
	{
		return services.AddDbContextPool<TContext>(options =>
		{
			if (migrationAssembly is null)
			{
				options.UseNpgsql(connectionString);
				return;
			}

			options.UseNpgsql(connectionString, b =>
				b.MigrationsAssembly(migrationAssembly));
		});
	}

	public static void ConvertToSnakeCase(this ModelBuilder builder)
    {
        foreach (var entity in builder.Model.GetEntityTypes())
        {
            entity.SetTableName(entity.GetTableName()!.ToSnakeCase());
          
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.GetColumnName().ToSnakeCase());
            }

            foreach (var key in entity.GetKeys())
            {
                key.SetName(key.GetName()!.ToSnakeCase());
            }

            foreach (var key in entity.GetForeignKeys())
            {
                key.SetConstraintName(key.GetConstraintName()!.ToSnakeCase());
            }

            foreach (var index in entity.GetIndexes())
            {
                index.SetDatabaseName(index.GetDatabaseName()!.ToSnakeCase());
            }
        }
    }

    private static string ToSnakeCase(this string input)
    {
        if (string.IsNullOrEmpty(input)) { return input; }

        var startUnderscores = Regex.Match(input, @"^_+");

        return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2")
            .ToLower(System.Globalization.CultureInfo.CurrentCulture);
    }
}