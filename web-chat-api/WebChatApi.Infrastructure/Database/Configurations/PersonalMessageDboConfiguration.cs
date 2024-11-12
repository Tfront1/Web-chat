using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Domain.Dbos;

namespace WebChatApi.Infrastructure.Database.Configurations;

public class PersonalMessageDboConfiguration : IEntityTypeConfiguration<PersonalMessageDbo>
{
	public void Configure(EntityTypeBuilder<PersonalMessageDbo> builder)
	{
		builder.HasKey(m => m.Id);
		builder.Property(m => m.Content).IsRequired();
		builder.HasOne(m => m.Author)
			.WithMany()
			.HasForeignKey(m => m.AuthorId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(m => m.Recipient)
			.WithMany()
			.HasForeignKey(m => m.RecipientId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}

