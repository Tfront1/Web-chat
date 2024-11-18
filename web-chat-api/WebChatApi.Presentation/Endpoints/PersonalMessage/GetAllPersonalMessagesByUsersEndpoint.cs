using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.PersonalMessage;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.PersonalMessage;

public class GetAllPersonalMessagesByUsersEndpoint : Endpoint<GetPersonalMessagesByUsersDto, ApiResponse>
{
	private readonly IPersonalMessageService _personalMessageService;
	public GetAllPersonalMessagesByUsersEndpoint(
		IPersonalMessageService personalMessageService)
	{
		_personalMessageService = personalMessageService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("get-all-by-users");
		Group<PersonalMessageEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("GetAll by users");
		});
		Summary(s =>
		{
			s.Summary = "Get all personal messages by users";
			s.Description = "Get all personal messages by users";
		});
	}

	public override async Task HandleAsync(GetPersonalMessagesByUsersDto req, CancellationToken ct)
	{
		Response = await _personalMessageService.GetAllPersonalMessagesByUsersAsync(req);
	}
}