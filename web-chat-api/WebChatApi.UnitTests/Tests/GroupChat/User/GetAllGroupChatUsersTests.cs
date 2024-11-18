using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Contracts.Dtos.GroupChat;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Models;
using WebChatApi.Infrastructure.Services.EntityServices;
using WebChatApi.UnitTests.Common;

namespace WebChatApi.UnitTests.Tests.GroupChat.User;

public class GetAllGroupChatUsersTests : BaseTest
{
	[Fact]
	public async Task GetAllGroupChatUsersByGroupChatIdAsync_Should_ReturnSuccessResponse()
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

		var createGroupChatDto = new CreateGroupChatDto
		{
			Name = "Name",
			Description = "Description",
			CreatorId = userId
		};

		int groupChatId = 1;

		await service.CreateGroupChatAsync(createGroupChatDto);

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
		var createUserDto3 = new CreateUserDto
		{
			Username = "Username3",
			FirstName = "FirstName3",
			LastName = "LastName3",
			City = "City3",
			Description = "Description3",
			Email = "Email3",
			PhoneNumber = "PhoneNumber3",
			GitHubUrl = "GitHubUrl3",
			LinkedInUrl = "LinkedInUrl3"
		};

		await userService.CreateUserAsync(createUserDto2);
		await userService.CreateUserAsync(createUserDto3);

		int user2Id = 2;
		int user3Id = 3;

		var groupChatUserDto1 = new GroupChatUserDto
		{
			GroupChatId = groupChatId,
			UserId = user2Id
		};
		var groupChatUserDto2 = new GroupChatUserDto
		{
			GroupChatId = groupChatId,
			UserId = user3Id
		};

		await service.CreateGroupChatUserAsync(groupChatUserDto1);
		await service.CreateGroupChatUserAsync(groupChatUserDto2);

		//Act
		var res = await service.GetAllGroupChatUsersByGroupChatIdAsync(groupChatId);

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		var groupChatUserList = res.Payload as List<GroupChatUserDto>;
		groupChatUserList.Should().NotBeNull();
		groupChatUserList.Should().HaveCount(2);
		groupChatUserList.Should().ContainSingle(u => u.UserId == user2Id);
		groupChatUserList.Should().ContainSingle(u => u.UserId == user3Id);
	}

	[Fact]
	public async Task GetAllGroupChatUsersByGroupChatIdAsync_Should_ReturnGroupChatNotFound()
	{
		//Arrange
		var service = new GroupChatService(_context);

		int groupChatId = 1;

		//Act
		var res = await service.GetAllGroupChatUsersByGroupChatIdAsync(groupChatId);

		//Assert
		res.Success.Should().BeFalse();
		res.Payload.Should().BeNull();
		res.ErrorMessage.Should().BeEquivalentTo(ProblemDetailsResponsesModel.GroupChatNotFound);
		(await _context.Users.FirstOrDefaultAsync(
			u => u.Id == groupChatId)).Should().BeNull();
	}
}
