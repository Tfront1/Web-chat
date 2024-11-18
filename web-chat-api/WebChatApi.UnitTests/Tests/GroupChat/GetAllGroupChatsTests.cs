using FluentAssertions;
using WebChatApi.Contracts.Dtos.GroupChat;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Infrastructure.Services.EntityServices;
using WebChatApi.UnitTests.Common;

namespace WebChatApi.UnitTests.Tests.GroupChat;

public class GetAllGroupChatsTests : BaseTest
{
	[Fact]
	public async Task GetAllAsync_Should_ReturnSuccessResponse()
	{
		//Arrange
		var userService = new UserService(_context);

		var createUserDto = new CreateUserDto
		{
			Username = "Username",
			FirstName = "FirstName",
			LastName = "LastName",
			City = "City",
			Description = "Description",
			Email = "Email",
			PhoneNumber = "PhoneNumber",
			GitHubUrl = "GitHubUrl",
			LinkedInUrl = "LinkedInUrl"
		};

		await userService.CreateUserAsync(createUserDto);

		int userId = 1;

		var service = new GroupChatService(_context);

		var createGroupChatDto1 = new CreateGroupChatDto
		{
			Name = "Name1",
			Description = "Description1",
			CreatorId = userId
		};
		var createGroupChatDto2 = new CreateGroupChatDto
		{
			Name = "Name2",
			Description = "Description2",
			CreatorId = userId
		};

		await service.CreateGroupChatAsync(createGroupChatDto1);
		await service.CreateGroupChatAsync(createGroupChatDto2);

		//Act
		var res = await service.GetAllAsync();

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		var groupChatList = res.Payload as List<GroupChatDto>;
		groupChatList.Should().NotBeNull();
		groupChatList.Should().HaveCount(2);
		groupChatList.Should().ContainSingle(u => u.Name == createGroupChatDto1.Name);
		groupChatList.Should().ContainSingle(u => u.Name == createGroupChatDto2.Name);
	}

	[Fact]
	public async Task GetAllAsync_Should_ReturnEmptySuccessResponse()
	{
		//Arrange
		var service = new GroupChatService(_context);

		//Act
		var res = await service.GetAllAsync();

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		var groupChatList = res.Payload as List<GroupChatDto>;
		groupChatList.Should().NotBeNull();
		groupChatList.Should().HaveCount(0);
	}
}
