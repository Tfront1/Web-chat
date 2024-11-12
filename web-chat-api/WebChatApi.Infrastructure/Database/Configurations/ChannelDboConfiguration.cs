using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Domain.Dbos;

namespace WebChatApi.Infrastructure.Database.Configurations;

public class ChannelDboConfiguration : IEntityTypeConfiguration<ChannelDbo>
{
	public void Configure(EntityTypeBuilder<ChannelDbo> builder)
	{
		builder.HasKey(c => c.Id);
		builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
		builder.Property(c => c.Description).HasMaxLength(500);

		builder.HasOne(c => c.Creator)
			.WithMany()
			.HasForeignKey(c => c.CreatorId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
