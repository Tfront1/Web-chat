using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Domain.Dbos;

namespace WebChatApi.Infrastructure.Database.Configurations;

public class GroupChatMessageDboConfiguration : IEntityTypeConfiguration<GroupChatMessageDbo>
{
	public void Configure(EntityTypeBuilder<GroupChatMessageDbo> builder)
	{
		builder.HasKey(m => m.Id);
		builder.Property(m => m.Content).IsRequired();
		builder.HasOne(m => m.Author)
			.WithMany()
			.HasForeignKey(m => m.AuthorId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(m => m.GroupChat)
			.WithMany(g => g.Messages)
			.HasForeignKey(m => m.GroupChatId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
