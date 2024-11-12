using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChatApi.Domain.Dbos;

public class ChannelUserDbo
{
	public int ChannelId { get; set; }
	public int UserId { get; set; }
	public bool IsAdmin { get; set; } = false;

	public virtual ChannelDbo Channel { get; set; } = null!;
	public virtual UserDbo User { get; set; } = null!;
}
