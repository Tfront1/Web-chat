using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.PersonalMessage;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.PersonalMessage;

public class CreatePersonalMessageEndpoint : Endpoint<CreatePersonalMessageDto, ApiResponse>
{
	private readonly IPersonalMessageService _personalMessageService;
	public CreatePersonalMessageEndpoint(
		IPersonalMessageService personalMessageService)
	{
		_personalMessageService = personalMessageService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("create");
		Group<PersonalMessageEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Create");
		});
		Summary(s =>
		{
			s.Summary = "Create personal message";
			s.Description = "Create personal message";
		});
	}

	public override async Task HandleAsync(CreatePersonalMessageDto req, CancellationToken ct)
	{
		Response = await _personalMessageService.CreatePersonalMessageAsync(req);
	}
}