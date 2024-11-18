using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Contracts.Dtos.Channel;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Models;
using WebChatApi.Infrastructure.Services.EntityServices;
using WebChatApi.UnitTests.Common;

namespace WebChatApi.UnitTests.Tests.Channel;

public class CreateChannelTests : BaseTest
{
	[Fact]
	public async Task CreateChannelAsync_Should_ReturnSuccessResponse()
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

		//Act
		var res = await service.CreateChannelAsync(createChannelDto);

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		res.Payload.Should().BeNull();
		(await _context.Channels.FirstOrDefaultAsync(
			u => u.Id == userId &&
			u.Name == createChannelDto.Name &&
			u.Description == createChannelDto.Description &&
			u.CreatorId == createChannelDto.CreatorId)).Should().NotBeNull();
		_context.Channels.Where(
			u => u.Id == channelId).Should().HaveCount(1);
	}

	[Fact]
	public async Task CreateChannelAsync_Should_ReturnUserNotFound()
	{
		//Arrange
		var service = new ChannelService(_context);

		var createChannelDto = new CreateChannelDto
		{
			Name = "Name",
			Description = "Description",
			CreatorId = 1
		};

		int channelId = 1;

		//Act
		var res = await service.CreateChannelAsync(createChannelDto);

		//Assert
		res.Success.Should().BeFalse();
		res.Payload.Should().BeNull();
		res.ErrorMessage.Should().BeEquivalentTo(ProblemDetailsResponsesModel.UserNotFound);
		_context.Channels.Where(
			u => u.Id == channelId).Should().HaveCount(0);
	}
}
