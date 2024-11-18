using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Requests;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.PersonalMessage;

public class DeletePersonalMessageEndpoint : Endpoint<IdRequest, ApiResponse>
{
	private readonly IPersonalMessageService _personalMessageService;
	public DeletePersonalMessageEndpoint(
		IPersonalMessageService personalMessageService)
	{
		_personalMessageService = personalMessageService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("delete");
		Group<PersonalMessageEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Delete");
		});
		Summary(s =>
		{
			s.Summary = "Delete personal message";
			s.Description = "Delete personal message";
		});
	}

	public override async Task HandleAsync(IdRequest req, CancellationToken ct)
	{
		Response = await _personalMessageService.DeleteAsync(req.Id);
	}
}