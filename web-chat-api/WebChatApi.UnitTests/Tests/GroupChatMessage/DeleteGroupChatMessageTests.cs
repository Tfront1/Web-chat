using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Contracts.Dtos.GroupChat;
using WebChatApi.Contracts.Dtos.GroupChatMessage;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Models;
using WebChatApi.Infrastructure.Services.EntityServices;
using WebChatApi.UnitTests.Common;

namespace WebChatApi.UnitTests.Tests.GroupChatMessage;

public class DeleteGroupChatMessageTests : BaseTest
{
	[Fact]
	public async Task DeleteAsync_Should_ReturnSuccessResponse()
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

		var createGroupChatMessageDto = new CreateGroupChatMessageDto
		{
			AuthorId = user2Id,
			GroupChatId = groupChatId,
			Content = "Content"
		};

		int groupChatMessageId = 1;

		await service.CreateGroupChatMessageAsync(createGroupChatMessageDto);

		//Act
		var res = await service.DeleteAsync(groupChatMessageId);

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		res.Payload.Should().BeNull();
		(await _context.GroupChatMessages.FirstOrDefaultAsync(
			u => u.AuthorId == createGroupChatMessageDto.AuthorId &&
			u.GroupChatId == createGroupChatMessageDto.GroupChatId &&
			u.Content == createGroupChatMessageDto.Content)).Should().BeNull();
		_context.GroupChatMessages.Where(
			u => u.Id == groupChatMessageId).Should().HaveCount(0);
	}

	[Fact]
	public async Task DeleteAsync_Should_ReturnEntityNotFound()
	{
		//Arrange
		var service = new GroupChatMessageService(_context);
		int groupChatMessageId = 1;

		//Act
		var res = await service.DeleteAsync(groupChatMessageId);

		//Assert
		res.Success.Should().BeFalse();
		res.Payload.Should().BeNull();
		res.ErrorMessage.Should().BeEquivalentTo(ProblemDetailsResponsesModel.EntityNotFound);
		(await _context.GroupChatMessages.FirstOrDefaultAsync(
			u => u.Id == groupChatMessageId)).Should().BeNull();
	}
}
