using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Contracts.Dtos.Channel;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Models;
using WebChatApi.Infrastructure.Services.EntityServices;
using WebChatApi.UnitTests.Common;

namespace WebChatApi.UnitTests.Tests.Channel;

public class DeleteChannelTests : BaseTest
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

		var service = new ChannelService(_context);

		var createChannelDto = new CreateChannelDto
		{
			Name = "Name",
			Description = "Description",
			CreatorId = userId
		};

		int channelId = 1;

		await service.CreateChannelAsync(createChannelDto);

		//Act
		var res = await service.DeleteAsync(channelId);

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		res.Payload.Should().BeNull();
		(await _context.Channels.FirstOrDefaultAsync(
			u => u.Id == channelId)).Should().BeNull();
	}

	[Fact]
	public async Task DeleteAsync_Should_ReturnEntityNotFound()
	{
		//Arrange
		var service = new ChannelService(_context);
		int channelId = 1;

		//Act
		var res = await service.DeleteAsync(channelId);

		//Assert
		res.Success.Should().BeFalse();
		res.Payload.Should().BeNull();
		res.ErrorMessage.Should().BeEquivalentTo(ProblemDetailsResponsesModel.EntityNotFound);
		(await _context.Channels.FirstOrDefaultAsync(
			u => u.Id == channelId)).Should().BeNull();
	}
}
