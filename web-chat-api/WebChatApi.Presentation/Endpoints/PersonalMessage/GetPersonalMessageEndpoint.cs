using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Requests;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.PersonalMessage;

public class GetPersonalMessageEndpoint : Endpoint<IdRequest, ApiResponse>
{
	private readonly IPersonalMessageService _personalMessageService;
	public GetPersonalMessageEndpoint(
		IPersonalMessageService personalMessageService)
	{
		_personalMessageService = personalMessageService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("get");
		Group<PersonalMessageEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Get");
		});
		Summary(s =>
		{
			s.Summary = "Get personal message";
			s.Description = "Get personal message";
		});
	}

	public override async Task HandleAsync(IdRequest req, CancellationToken ct)
	{
		Response = await _personalMessageService.GetByIdAsync(req.Id);
	}
}