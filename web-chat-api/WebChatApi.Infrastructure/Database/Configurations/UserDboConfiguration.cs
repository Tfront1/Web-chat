using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace WebChatApi.Infrastructure.Database.Configurations;

public class UserDboConfiguration : IEntityTypeConfiguration<UserDbo>
{
	public void Configure(EntityTypeBuilder<UserDbo> builder)
	{
		builder.HasKey(u => u.Id);
		builder.Property(u => u.Username).IsRequired().HasMaxLength(50);
		builder.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
		builder.Property(u => u.LastName).IsRequired().HasMaxLength(50);
		builder.Property(u => u.Email).HasMaxLength(100);
		builder.Property(u => u.Description).HasMaxLength(500);
	}
}

