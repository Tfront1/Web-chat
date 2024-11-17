using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.GroupChat;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.GroupChat.User;

public class CreateGroupChatUserEndpoint : Endpoint<GroupChatUserDto, ApiResponse>
{
    private readonly IGroupChatService _groupChatService;
    public CreateGroupChatUserEndpoint(
        IGroupChatService groupChatService)
    {
        _groupChatService = groupChatService;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Post("create");
        Group<GroupChatUserEndpointsGroup>();
        Description(d =>
        {
            d.WithDisplayName("Create user");
        });
        Summary(s =>
        {
            s.Summary = "Create group chat user";
            s.Description = "Create group chat user";
        });
    }

    public override async Task HandleAsync(GroupChatUserDto req, CancellationToken ct)
    {
        Response = await _groupChatService.CreateGroupChatUserAsync(req);
    }
}