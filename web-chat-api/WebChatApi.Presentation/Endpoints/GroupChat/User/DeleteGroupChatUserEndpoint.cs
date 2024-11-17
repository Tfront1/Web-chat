using FastEndpoints;
using WebChatApi.Application.Services.EntityServices;
using WebChatApi.Contracts.Dtos.GroupChat;
using WebChatApi.Contracts.Responses;
using WebChatApi.Infrastructure.EndpointSettings.Groups;

namespace WebChatApi.Presentation.Endpoints.GroupChat.User;

public class DeleteGroupChatUserEndpoint : Endpoint<GroupChatUserDto, ApiResponse>
{
    private readonly IGroupChatService _groupChatService;
    public DeleteGroupChatUserEndpoint(
        IGroupChatService groupChatService)
    {
        _groupChatService = groupChatService;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Post("delete");
        Group<GroupChatUserEndpointsGroup>();
        Description(d =>
        {
            d.WithDisplayName("Delete user");
        });
        Summary(s =>
        {
            s.Summary = "Delete group chat user";
            s.Description = "Delete group chat user";
        });
    }

    public override async Task HandleAsync(GroupChatUserDto req, CancellationToken ct)
    {
        Response = await _groupChatService.DeleteGroupChatUserAsync(req);
    }
}