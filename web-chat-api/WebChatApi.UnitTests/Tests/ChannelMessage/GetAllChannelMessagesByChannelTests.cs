using FluentAssertions;
using WebChatApi.Contracts.Dtos.Channel;
using WebChatApi.Contracts.Dtos.ChannelMessage;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Models;
using WebChatApi.Infrastructure.Services.EntityServices;
using WebChatApi.UnitTests.Common;

namespace WebChatApi.UnitTests.Tests.ChannelMessage;

public class GetAllChannelMessagesByChannelTests : BaseTest
{
	[Fact]
	public async Task GetAllChannelMessagesByChannelAsync_Should_ReturnSuccessResponse()
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

		var channelservice = new ChannelService(_context);

		var createChannelDto = new CreateChannelDto
		{
			Name = "Name",
			Description = "Description",
			CreatorId = user1Id
		};

		int channelId = 1;

		await channelservice.CreateChannelAsync(createChannelDto);

		var service = new ChannelMessageService(_context);

		var createChannelMessageDto1 = new CreateChannelMessageDto
		{
			AuthorId = user2Id,
			ChannelId = channelId,
			Content = "Content1"
		};
		var createChannelMessageDto2 = new CreateChannelMessageDto
		{
			AuthorId = user2Id,
			ChannelId = channelId,
			Content = "Content2"
		};

		await service.CreateChannelMessageAsync(createChannelMessageDto1);
		await service.CreateChannelMessageAsync(createChannelMessageDto2);

		int channelMessage1Id = 1;
		int channelMessage2Id = 2;

		var getChannelMessagesByChannelDto = new GetChannelMessagesByChannelDto
		{
			ChannelId = channelId
		};

		//Act
		var res = await service.GetAllChannelMessagesByChannelAsync(getChannelMessagesByChannelDto);

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		var channelMessageList = res.Payload as List<ChannelMessageDto>;
		channelMessageList.Should().NotBeNull();
		channelMessageList.Should().HaveCount(2);
		channelMessageList.Should().ContainSingle(u => u.Id == channelMessage1Id);
		channelMessageList.Should().ContainSingle(u => u.Id == channelMessage2Id);
	}

	[Fact]
	public async Task GetAllChannelMessagesByChannelAsync_Should_ReturnChannelNotFound()
	{
		//Arrange
		int channelId = 1;

		var service = new ChannelMessageService(_context);

		var getChannelMessagesByChannelDto = new GetChannelMessagesByChannelDto
		{
			ChannelId = channelId
		};

		//Act
		var res = await service.GetAllChannelMessagesByChannelAsync(getChannelMessagesByChannelDto);

		//Assert
		res.Success.Should().BeFalse();
		res.Payload.Should().BeNull();
		res.ErrorMessage.Should().BeEquivalentTo(ProblemDetailsResponsesModel.ChannelNotFound);
	}
}
