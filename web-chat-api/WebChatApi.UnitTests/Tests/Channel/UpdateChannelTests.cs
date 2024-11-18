using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Contracts.Dtos.Channel;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Models;
using WebChatApi.Infrastructure.Services.EntityServices;
using WebChatApi.UnitTests.Common;

namespace WebChatApi.UnitTests.Tests.Channel;

public class UpdateChannelTests : BaseTest
{
	[Fact]
	public async Task UpdateChannelAsync_Should_ReturnSuccessResponse()
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

		var updateChannelDto = new UpdateChannelDto
		{
			Id = channelId,
			Name = "Name1",
			Description = "Description1",
		};

		//Act
		var res = await service.UpdateChannelAsync(updateChannelDto);

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		res.Payload.Should().BeNull();
		(await _context.Channels.FirstOrDefaultAsync(
			u => u.Id == channelId &&
			u.Name == updateChannelDto.Name &&
			u.Description == updateChannelDto.Description)).Should().NotBeNull();
		_context.Channels.Where(
			u => u.Id == channelId).Should().HaveCount(1);
	}

	[Fact]
	public async Task UpdateChannelAsync_Should_ReturnChannelNotFound()
	{
		//Arrange
		var service = new ChannelService(_context);

		int channelId = 1;

		var updateChannelDto = new UpdateChannelDto
		{
			Id = channelId,
			Name = "Name1",
			Description = "Description1",
		};

		//Act
		var res = await service.UpdateChannelAsync(updateChannelDto);

		//Assert
		res.Success.Should().BeFalse();
		res.Payload.Should().BeNull();
		res.ErrorMessage.Should().BeEquivalentTo(ProblemDetailsResponsesModel.ChannelNotFound);
		(await _context.Channels.FirstOrDefaultAsync(
			u => u.Id == channelId)).Should().BeNull();
	}
}
