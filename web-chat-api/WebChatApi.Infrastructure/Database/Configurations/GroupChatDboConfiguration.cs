using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Domain.Dbos;

namespace WebChatApi.Infrastructure.Database.Configurations;

public class GroupChatDboConfiguration : IEntityTypeConfiguration<GroupChatDbo>
{
	public void Configure(EntityTypeBuilder<GroupChatDbo> builder)
	{
		builder.HasKey(g => g.Id);
		builder.Property(g => g.Name).IsRequired().HasMaxLength(100);
		builder.Property(g => g.Description).HasMaxLength(500);

		builder.HasOne(g => g.Creator)
			.WithMany()
			.HasForeignKey(g => g.CreatorId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}