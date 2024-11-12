using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Domain.Dbos;

namespace WebChatApi.Infrastructure.Database.Configurations;

public class ChannelUserDboConfiguration : IEntityTypeConfiguration<ChannelUserDbo>
{
	public void Configure(EntityTypeBuilder<ChannelUserDbo> builder)
	{
		builder.HasKey(gm => new { gm.ChannelId, gm.UserId });

		builder.Property(gm => gm.IsAdmin).IsRequired();

		builder.HasOne(gm => gm.Channel)
			.WithMany(g => g.Members)
			.HasForeignKey(gm => gm.ChannelId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasOne(gm => gm.User)
			.WithMany(u => u.ChannelsMember)
			.HasForeignKey(gm => gm.UserId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}