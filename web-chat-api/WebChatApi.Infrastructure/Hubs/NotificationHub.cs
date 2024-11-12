using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace WebChatApi.Infrastructure.Hubs;

public class NotificationHub : Hub<INotificationClient>
{
	//public Task SendMessage(MessageModel message)
	//{
	//	//var message1 = new MessageModel()
	//	//{
	//	//	Id = 1,
	//	//	AuthorId = 1,
	//	//	Content = "Vafla",
	//	//	CreatedAt = DateTime.UtcNow,
	//	//	IsEdited = false,
	//	//};

	//	//return Clients.Others.Send(message1);
	//	return Task.CompletedTask;
	//}

	public override Task OnConnectedAsync()
	{
		////var message1 = new MessageModel()
		////{
		////	Id = 1,
		////	AuthorId = 1,
		////	Content = "Connect",
		////	CreatedAt = DateTime.UtcNow,
		////	IsEdited = false,
		////};

		//Clients.Others.Send(message1);
		//return base.OnConnectedAsync();
		return Task.CompletedTask;
	}

	public override Task OnDisconnectedAsync(Exception? exception)
	{
		//var message1 = new MessageModel()
		//{
		//	Id = 1,
		//	AuthorId = 1,
		//	Content = "Disconect",
		//	CreatedAt = DateTime.UtcNow,
		//	IsEdited = false,
		//};

		//Clients.Others.Send(message1);
		//return base.OnDisconnectedAsync(exception);
		return Task.CompletedTask;
	}

	protected override void Dispose(bool disposing)
	{
		Debug.WriteLine("Hub disposing");
		base.Dispose(disposing);
	}
}