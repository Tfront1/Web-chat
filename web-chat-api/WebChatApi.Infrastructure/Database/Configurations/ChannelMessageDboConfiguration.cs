using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Domain.Dbos;

namespace WebChatApi.Infrastructure.Database.Configurations;

public class ChannelMessageDboConfiguration : IEntityTypeConfiguration<ChannelMessageDbo>
{
	public void Configure(EntityTypeBuilder<ChannelMessageDbo> builder)
	{
		builder.HasKey(m => m.Id);
		builder.Property(m => m.Content).IsRequired();
		builder.HasOne(m => m.Author)
			.WithMany()
			.HasForeignKey(m => m.AuthorId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(m => m.Channel)
			.WithMany(c => c.Messages)
			.HasForeignKey(m => m.ChannelId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
