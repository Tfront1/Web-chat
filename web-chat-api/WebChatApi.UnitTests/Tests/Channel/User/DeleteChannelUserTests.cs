using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Contracts.Dtos.Channel;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Models;
using WebChatApi.Infrastructure.Services.EntityServices;
using WebChatApi.UnitTests.Common;

namespace WebChatApi.UnitTests.Tests.Channel.User;

public class DeleteChannelUserTests : BaseTest
{
	[Fact]
	public async Task DeleteChannelUserAsync_Should_ReturnSuccessResponse()
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

		var channelUserDto = new ChannelUserDto
		{
			ChannelId = channelId,
			UserId = user2Id
		};

		await service.CreateChannelUserAsync(channelUserDto);
		//Act
		var res = await service.DeleteChannelUserAsync(channelUserDto);

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		res.Payload.Should().BeNull();
		(await _context.ChannelUsers.FirstOrDefaultAsync(
			u => u.UserId == channelUserDto.UserId && u.ChannelId == channelUserDto.ChannelId)
			).Should().BeNull();
	}

	[Fact]
	public async Task DeleteChannelUserAsync_Should_ReturnChannelUserNotFound()
	{
		//Arrange

		var service = new ChannelService(_context);

		int channelId = 1;
		int userId = 1;

		var channelUserDto = new ChannelUserDto
		{
			ChannelId = channelId,
			UserId = userId
		};

		//Act
		var res = await service.DeleteChannelUserAsync(channelUserDto);

		//Assert
		res.Success.Should().BeFalse();
		res.Payload.Should().BeNull();
		res.ErrorMessage.Should().BeEquivalentTo(ProblemDetailsResponsesModel.ChannelUserNotFound);
		(await _context.ChannelUsers.FirstOrDefaultAsync(
			u => u.UserId == channelUserDto.UserId && u.ChannelId == channelUserDto.ChannelId)
			).Should().BeNull();
	}
}
