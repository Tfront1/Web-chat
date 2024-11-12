using Microsoft.EntityFrameworkCore;
using WebChatApi.Domain.Dbos;
using WebChatApi.Infrastructure.Database.Configurations;
using WebChatApi.Infrastructure.Extensions.Database;

namespace WebChatApi.Infrastructure.Database;

public class WebChatApiInternalContext : DbContext
{
	public WebChatApiInternalContext(DbContextOptions<WebChatApiInternalContext> options)
		: base(options)
	{
	}

	public virtual DbSet<UserDbo> Users { get; set; } = null!;
	public virtual DbSet<ChannelDbo> Channels { get; set; } = null!;
	public virtual DbSet<ChannelUserDbo> ChannelUsers { get; set; } = null!;
	public virtual DbSet<GroupChatDbo> GroupChats { get; set; } = null!;
	public virtual DbSet<GroupChatUserDbo> GroupChatUsers { get; set; } = null!;
	public virtual DbSet<ChannelMessageDbo> ChannelMessages { get; set; } = null!;
	public virtual DbSet<GroupChatMessageDbo> GroupChatMessages { get; set; } = null!;
	public virtual DbSet<PersonalMessageDbo> PersonalMessages { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new UserDboConfiguration());
		modelBuilder.ApplyConfiguration(new ChannelDboConfiguration());
		modelBuilder.ApplyConfiguration(new ChannelUserDboConfiguration());
		modelBuilder.ApplyConfiguration(new GroupChatDboConfiguration());
		modelBuilder.ApplyConfiguration(new GroupChatUserDboConfiguration());
		modelBuilder.ApplyConfiguration(new ChannelMessageDboConfiguration());
		modelBuilder.ApplyConfiguration(new GroupChatMessageDboConfiguration());
		modelBuilder.ApplyConfiguration(new PersonalMessageDboConfiguration());

		modelBuilder.ConvertToSnakeCase();
	}
}