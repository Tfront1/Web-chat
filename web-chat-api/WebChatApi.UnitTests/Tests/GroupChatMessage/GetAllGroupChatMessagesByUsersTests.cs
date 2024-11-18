using FluentAssertions;
using WebChatApi.Contracts.Dtos.GroupChat;
using WebChatApi.Contracts.Dtos.GroupChatMessage;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Models;
using WebChatApi.Infrastructure.Services.EntityServices;
using WebChatApi.UnitTests.Common;

namespace WebChatApi.UnitTests.Tests.GroupChatMessage;

public class GetAllGroupChatMessagesByUsersTests : BaseTest
{
	[Fact]
	public async Task GetAllGroupChatMessagesByGroupAsync_Should_ReturnSuccessResponse()
	{
		//Arrange
		var userService = new UserService(_context);

		var createUserDto1 = new CreateUserDto
		{
			Username = "Username1",
			FirstName = "FirstName1",
			LastName = "LastName1",
			City = "City1",
			Description = "Description1",
			Email = "Email1",
			PhoneNumber = "PhoneNumber1",
			GitHubUrl = "GitHubUrl1",
			LinkedInUrl = "LinkedInUrl1"
		};
		var createUserDto2 = new CreateUserDto
		{
			Username = "Username2",
			FirstName = "FirstName2",
			LastName = "LastName2",
			City = "City2",
			Description = "Description2",
			Email = "Email2",
			PhoneNumber = "PhoneNumber2",
			GitHubUrl = "GitHubUrl2",
			LinkedInUrl = "LinkedInUrl2"
		};

		await userService.CreateUserAsync(createUserDto1);
		await userService.CreateUserAsync(createUserDto2);

		int user1Id = 1;
		int user2Id = 2;

		var groupChatservice = new GroupChatService(_context);

		var createGroupChatDto = new CreateGroupChatDto
		{
			Name = "Name",
			Description = "Description",
			CreatorId = user1Id
		};

		int groupChatId = 1;

		await groupChatservice.CreateGroupChatAsync(createGroupChatDto);

		var service = new GroupChatMessageService(_context);

		var createGroupChatMessageDto1 = new CreateGroupChatMessageDto
		{
			AuthorId = user2Id,
			GroupChatId = groupChatId,
			Content = "Content1"
		};
		var createGroupChatMessageDto2 = new CreateGroupChatMessageDto
		{
			AuthorId = user2Id,
			GroupChatId = groupChatId,
			Content = "Content2"
		};

		await service.CreateGroupChatMessageAsync(createGroupChatMessageDto1);
		await service.CreateGroupChatMessageAsync(createGroupChatMessageDto2);

		int groupChatMessage1Id = 1;
		int groupChatMessage2Id = 2;

		var getGroupChatMessagesByGroupDto = new GetGroupChatMessagesByGroupDto
		{
			GroupChatId = groupChatId
		};

		//Act
		var res = await service.GetAllGroupChatMessagesByGroupAsync(getGroupChatMessagesByGroupDto);

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		var groupChatMessageList = res.Payload as List<GroupChatMessageDto>;
		groupChatMessageList.Should().NotBeNull();
		groupChatMessageList.Should().HaveCount(2);
		groupChatMessageList.Should().ContainSingle(u => u.Id == groupChatMessage1Id);
		groupChatMessageList.Should().ContainSingle(u => u.Id == groupChatMessage2Id);
	}

	[Fact]
	public async Task GetAllGroupChatMessagesByGroupAsync_Should_ReturnGroupChatNotFound()
	{
		//Arrange
		int groupChatId = 1;

		var service = new GroupChatMessageService(_context);

		var getGroupChatMessagesByGroupDto = new GetGroupChatMessagesByGroupDto
		{
			GroupChatId = groupChatId
		};

		//Act
		var res = await service.GetAllGroupChatMessagesByGroupAsync(getGroupChatMessagesByGroupDto);

		//Assert
		res.Success.Should().BeFalse();
		res.Payload.Should().BeNull();
		res.ErrorMessage.Should().BeEquivalentTo(ProblemDetailsResponsesModel.GroupChatNotFound);
	}
}
