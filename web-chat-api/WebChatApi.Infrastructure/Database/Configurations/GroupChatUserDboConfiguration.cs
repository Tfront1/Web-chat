using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Domain.Dbos;

namespace WebChatApi.Infrastructure.Database.Configurations;

public class GroupChatUserDboConfiguration : IEntityTypeConfiguration<GroupChatUserDbo>
{
	public void Configure(EntityTypeBuilder<GroupChatUserDbo> builder)
	{
		builder.HasKey(gm => new { gm.GroupChatId, gm.UserId });

		builder.HasOne(gm => gm.GroupChat)
			.WithMany(g => g.Members)
			.HasForeignKey(gm => gm.GroupChatId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasOne(gm => gm.User)
			.WithMany(u => u.GroupChatsMember)
			.HasForeignKey(gm => gm.UserId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}