using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.PersonalMessage;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.PersonalMessage;

public class UpdatePersonalMessageContentEndpoint : Endpoint<UpdatePersonalMessageContentDto, ApiResponse>
{
	private readonly IPersonalMessageService _personalMessageService;
	public UpdatePersonalMessageContentEndpoint(
		IPersonalMessageService personalMessageService)
	{
		_personalMessageService = personalMessageService;
	}

	public override void Configure()
	{
		AllowAnonymous();
		Post("update-content");
		Group<PersonalMessageEndpointsGroup>();
		Description(d =>
		{
			d.WithDisplayName("Update content");
		});
		Summary(s =>
		{
			s.Summary = "Update personal message content";
			s.Description = "Update personal message content";
		});
	}

	public override async Task HandleAsync(UpdatePersonalMessageContentDto req, CancellationToken ct)
	{
		Response = await _personalMessageService.UpdatePersonalMessageContentAsync(req);
	}
}