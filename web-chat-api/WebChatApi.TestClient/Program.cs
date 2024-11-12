using Microsoft.AspNetCore.SignalR.Client;

namespace WebChatApi.TestClient;

internal class Program
{
	static HubConnection hubConnection;
	static async Task Main(string[] args)
	{
		await InitSignalRConnection();

		bool isExit = false;
		while (!isExit)
		{
			Console.WriteLine("Enter message or exit.");
			var input = Console.ReadLine();

			if (string.IsNullOrWhiteSpace(input))
			{
				continue;
			}

			if (input == "exit")
			{
				isExit = true;
			}
			else
			{
				//var message1 = new MessageModel()
				//{
				//	Id = 1,
				//	AuthorId = 1,
				//	Content = "SendMass",
				//	CreatedAt = DateTime.UtcNow,
				//	IsEdited = false,
				//};

				//await hubConnection.SendAsync("SendMessage", message1);
			}
		}
	}
	private static Task InitSignalRConnection()
	{
		hubConnection = new HubConnectionBuilder()
			.WithUrl("https://localhost:7155/notification-hub")
			.Build();

		//hubConnection.On<MessageModel>("Send", message =>
		//{
		//	Console.WriteLine(message.Content + "\n" + message.Content);
		//});

		return hubConnection.StartAsync();
	}
}
