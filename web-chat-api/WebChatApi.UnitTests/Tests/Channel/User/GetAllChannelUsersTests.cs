using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Contracts.Dtos.Channel;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Models;
using WebChatApi.Infrastructure.Services.EntityServices;
using WebChatApi.UnitTests.Common;

namespace WebChatApi.UnitTests.Tests.Channel.User;

public class GetAllChannelUsersTests : BaseTest
{
	[Fact]
	public async Task GetAllChannelUsersByChannelIdAsync_Should_ReturnSuccessResponse()
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

		var channelUserDto1 = new ChannelUserDto
		{
			ChannelId = channelId,
			UserId = user2Id
		};
		var channelUserDto2 = new ChannelUserDto
		{
			ChannelId = channelId,
			UserId = user3Id
		};

		await service.CreateChannelUserAsync(channelUserDto1);
		await service.CreateChannelUserAsync(channelUserDto2);

		//Act
		var res = await service.GetAllChannelUsersByChannelIdAsync(channelId);

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		var channelUserList = res.Payload as List<ChannelUserDto>;
		channelUserList.Should().NotBeNull();
		channelUserList.Should().HaveCount(2);
		channelUserList.Should().ContainSingle(u => u.UserId == user2Id);
		channelUserList.Should().ContainSingle(u => u.UserId == user3Id);
	}

	[Fact]
	public async Task GetAllChannelUsersByChannelIdAsync_Should_ReturnChannelNotFound()
	{
		//Arrange
		var service = new ChannelService(_context);

		int channelId = 1;

		//Act
		var res = await service.GetAllChannelUsersByChannelIdAsync(channelId);

		//Assert
		res.Success.Should().BeFalse();
		res.Payload.Should().BeNull();
		res.ErrorMessage.Should().BeEquivalentTo(ProblemDetailsResponsesModel.ChannelNotFound);
		(await _context.Users.FirstOrDefaultAsync(
			u => u.Id == channelId)).Should().BeNull();
	}
}
