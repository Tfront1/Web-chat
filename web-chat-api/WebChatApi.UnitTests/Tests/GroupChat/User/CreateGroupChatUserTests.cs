using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Contracts.Dtos.GroupChat;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Models;
using WebChatApi.Infrastructure.Services.EntityServices;
using WebChatApi.UnitTests.Common;

namespace WebChatApi.UnitTests.Tests.GroupChat.User;

public class CreateGroupChatUserTests : BaseTest
{
	[Fact]
	public async Task CreateGroupChatUserAsync_Should_ReturnSuccessResponse()
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

		await userService.CreateUserAsync(createUserDto2);

		int user2Id = 2;

		var groupChatUserDto = new GroupChatUserDto
		{
			GroupChatId = groupChatId,
			UserId = user2Id
		};

		//Act
		var res = await service.CreateGroupChatUserAsync(groupChatUserDto);

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		res.Payload.Should().BeNull();
		(await _context.GroupChatUsers.FirstOrDefaultAsync(
			u => u.UserId == user2Id &&
			u.GroupChatId == groupChatId)).Should().NotBeNull();
	}

	[Fact]
	public async Task CreateGroupChatUserAsync_Should_ReturnGroupChatUserAlreadyExist()
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

		await userService.CreateUserAsync(createUserDto2);

		int user2Id = 2;

		var groupChatUserDto = new GroupChatUserDto
		{
			GroupChatId = groupChatId,
			UserId = user2Id
		};

		await service.CreateGroupChatUserAsync(groupChatUserDto);
		//Act
		var res = await service.CreateGroupChatUserAsync(groupChatUserDto);

		//Assert
		res.Success.Should().BeFalse();
		res.Payload.Should().BeNull();
		res.ErrorMessage.Should().BeEquivalentTo(ProblemDetailsResponsesModel.GroupChatUserAlreadyExist);
		_context.GroupChatUsers.Where(
			u => u.GroupChatId == groupChatId && u.UserId == user2Id).Should().HaveCount(1);
	}

	[Fact]
	public async Task CreateGroupChatUserAsync_Should_ReturnGroupChatNotFound()
	{
		//Arrange
		var service = new GroupChatService(_context);

		int userId = 1;
		int groupChatId = 1;

		var groupChatUserDto = new GroupChatUserDto
		{
			GroupChatId = groupChatId,
			UserId = userId
		};

		//Act
		var res = await service.CreateGroupChatUserAsync(groupChatUserDto);

		//Assert
		res.Success.Should().BeFalse();
		res.Payload.Should().BeNull();
		res.ErrorMessage.Should().BeEquivalentTo(ProblemDetailsResponsesModel.GroupChatNotFound);
	}

	[Fact]
	public async Task CreateGroupChatUserAsync_Should_ReturnUserNotFound()
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

		int user2Id = 2;

		var groupChatUserDto = new GroupChatUserDto
		{
			GroupChatId = groupChatId,
			UserId = user2Id
		};

		await service.CreateGroupChatUserAsync(groupChatUserDto);
		//Act
		var res = await service.CreateGroupChatUserAsync(groupChatUserDto);

		//Assert
		res.Success.Should().BeFalse();
		res.Payload.Should().BeNull();
		res.ErrorMessage.Should().BeEquivalentTo(ProblemDetailsResponsesModel.UserNotFound);
	}
}
