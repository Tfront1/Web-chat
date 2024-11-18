using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Contracts.Dtos.GroupChat;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Models;
using WebChatApi.Infrastructure.Services.EntityServices;
using WebChatApi.UnitTests.Common;

namespace WebChatApi.UnitTests.Tests.GroupChat;

public class DeleteGroupChatTests : BaseTest
{
	[Fact]
	public async Task DeleteAsync_Should_ReturnSuccessResponse()
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

		//Act
		var res = await service.DeleteAsync(groupChatId);

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		res.Payload.Should().BeNull();
		(await _context.GroupChats.FirstOrDefaultAsync(
			u => u.Id == groupChatId)).Should().BeNull();
	}

	[Fact]
	public async Task DeleteAsync_Should_ReturnEntityNotFound()
	{
		//Arrange
		var service = new GroupChatService(_context);
		int groupChatId = 1;

		//Act
		var res = await service.DeleteAsync(groupChatId);

		//Assert
		res.Success.Should().BeFalse();
		res.Payload.Should().BeNull();
		res.ErrorMessage.Should().BeEquivalentTo(ProblemDetailsResponsesModel.EntityNotFound);
		(await _context.GroupChats.FirstOrDefaultAsync(
			u => u.Id == groupChatId)).Should().BeNull();
	}
}
