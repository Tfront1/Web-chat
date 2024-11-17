using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChatApi.Contracts.Dtos.GroupChat;

public class GroupChatUserDto
{
	public int GroupChatId { get; set; }
	public int UserId { get; set; }
}
