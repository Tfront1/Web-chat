using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Contracts.Dtos.GroupChat;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Models;
using WebChatApi.Infrastructure.Services.EntityServices;
using WebChatApi.UnitTests.Common;

namespace WebChatApi.UnitTests.Tests.GroupChat.User;

public class DeleteGroupChatUserTests : BaseTest
{
	[Fact]
	public async Task DeleteGroupChatUserAsync_Should_ReturnSuccessResponse()
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
		var res = await service.DeleteGroupChatUserAsync(groupChatUserDto);

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		res.Payload.Should().BeNull();
		(await _context.GroupChatUsers.FirstOrDefaultAsync(
			u => u.UserId == groupChatUserDto.UserId && u.GroupChatId == groupChatUserDto.GroupChatId)
			).Should().BeNull();
	}

	[Fact]
	public async Task DeleteGroupChatUserAsync_Should_ReturnGroupChatUserNotFound()
	{
		//Arrange

		var service = new GroupChatService(_context);

		int groupChatId = 1;
		int userId = 1;

		var groupChatUserDto = new GroupChatUserDto
		{
			GroupChatId = groupChatId,
			UserId = userId
		};

		//Act
		var res = await service.DeleteGroupChatUserAsync(groupChatUserDto);

		//Assert
		res.Success.Should().BeFalse();
		res.Payload.Should().BeNull();
		res.ErrorMessage.Should().BeEquivalentTo(ProblemDetailsResponsesModel.GroupChatUserNotFound);
		(await _context.GroupChatUsers.FirstOrDefaultAsync(
			u => u.UserId == groupChatUserDto.UserId && u.GroupChatId == groupChatUserDto.GroupChatId)
			).Should().BeNull();
	}
}
