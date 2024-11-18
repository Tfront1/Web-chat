using FluentAssertions;
using WebChatApi.Contracts.Dtos.Channel;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Models;
using WebChatApi.Infrastructure.Services.EntityServices;
using WebChatApi.UnitTests.Common;

namespace WebChatApi.UnitTests.Tests.Channel;

public class GetChannelTests : BaseTest
{
	[Fact]
	public async Task GetByIdAsync_Should_ReturnSuccessResponse()
	{
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
		var res = await service.GetByIdAsync(channelId);

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		var channel = res.Payload as ChannelDto;
		channel.Should().NotBeNull();
		channel.Should().Match<ChannelDto>(g =>
			g.Id == channelId &&
			g.Name == createChannelDto.Name &&
			g.Description == createChannelDto.Description &&
			g.CreatorId == createChannelDto.CreatorId &&
			g.CreatedAt != default
		);
	}

	[Fact]
	public async Task GetByIdAsync_Should_ReturnEntityNotFound()
	{
		//Arrange
		var service = new ChannelService(_context);

		int channelId = 1;

		//Act
		var res = await service.GetByIdAsync(channelId);

		//Assert
		res.Success.Should().BeFalse();
		res.Payload.Should().BeNull();
		res.ErrorMessage.Should().BeEquivalentTo(ProblemDetailsResponsesModel.EntityNotFound);
	}
}
