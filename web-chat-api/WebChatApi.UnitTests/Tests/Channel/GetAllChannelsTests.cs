using FluentAssertions;
using WebChatApi.Contracts.Dtos.Channel;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Infrastructure.Services.EntityServices;
using WebChatApi.UnitTests.Common;

namespace WebChatApi.UnitTests.Tests.Channel;

public class GetAllChannelsTests : BaseTest
{
	[Fact]
	public async Task GetAllAsync_Should_ReturnSuccessResponse()
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

		var createChannelDto1 = new CreateChannelDto
		{
			Name = "Name1",
			Description = "Description1",
			CreatorId = userId
		};
		var createChannelDto2 = new CreateChannelDto
		{
			Name = "Name2",
			Description = "Description2",
			CreatorId = userId
		};

		await service.CreateChannelAsync(createChannelDto1);
		await service.CreateChannelAsync(createChannelDto2);

		//Act
		var res = await service.GetAllAsync();

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		var channelList = res.Payload as List<ChannelDto>;
		channelList.Should().NotBeNull();
		channelList.Should().HaveCount(2);
		channelList.Should().ContainSingle(u => u.Name == createChannelDto1.Name);
		channelList.Should().ContainSingle(u => u.Name == createChannelDto2.Name);
	}

	[Fact]
	public async Task GetAllAsync_Should_ReturnEmptySuccessResponse()
	{
		//Arrange
		var service = new ChannelService(_context);

		//Act
		var res = await service.GetAllAsync();

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		var channelList = res.Payload as List<ChannelDto>;
		channelList.Should().NotBeNull();
		channelList.Should().HaveCount(0);
	}
}
