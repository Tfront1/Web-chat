using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Contracts.Dtos.GroupChat;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Models;
using WebChatApi.Infrastructure.Services.EntityServices;
using WebChatApi.UnitTests.Common;

namespace WebChatApi.UnitTests.Tests.GroupChat;

public class CreateGroupChatTests : BaseTest
{
	[Fact]
	public async Task CreateGroupChatAsync_Should_ReturnSuccessResponse()
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

		//Act
		var res = await service.CreateGroupChatAsync(createGroupChatDto);

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		res.Payload.Should().BeNull();
		(await _context.GroupChats.FirstOrDefaultAsync(
			u => u.Id == userId &&
			u.Name == createGroupChatDto.Name &&
			u.Description == createGroupChatDto.Description &&
			u.CreatorId == createGroupChatDto.CreatorId)).Should().NotBeNull();
		_context.GroupChats.Where(
			u => u.Id == groupChatId).Should().HaveCount(1);
	}

	[Fact]
	public async Task CreateGroupChatAsync_Should_ReturnUserNotFound()
	{
		//Arrange
		var service = new GroupChatService(_context);

		var createGroupChatDto = new CreateGroupChatDto
		{
			Name = "Name",
			Description = "Description",
			CreatorId = 1
		};

		int groupChatId = 1;

		//Act
		var res = await service.CreateGroupChatAsync(createGroupChatDto);

		//Assert
		res.Success.Should().BeFalse();
		res.Payload.Should().BeNull();
		res.ErrorMessage.Should().BeEquivalentTo(ProblemDetailsResponsesModel.UserNotFound);
		_context.GroupChats.Where(
			u => u.Id == groupChatId).Should().HaveCount(0);
	}
}
