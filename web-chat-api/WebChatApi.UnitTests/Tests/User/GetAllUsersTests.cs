using FluentAssertions;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Infrastructure.Services.EntityServices;
using WebChatApi.UnitTests.Common;

namespace WebChatApi.UnitTests.Tests.User;

public class GetAllUsersTests : BaseTest
{
	[Fact]
	public async Task GetAllAsync_Should_ReturnSuccessResponse()
	{
		//Arrange
		var service = new UserService(_context);

		var createUserDto1 = new CreateUserDto
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

		var createUserDto2 = new CreateUserDto
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

		await service.CreateUserAsync(createUserDto1);
		await service.CreateUserAsync(createUserDto2);

		//Act
		var res = await service.GetAllAsync();

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		var userList = res.Payload as List<UserDto>;
		userList.Should().NotBeNull();
		userList.Should().HaveCount(2);
		userList.Should().ContainSingle(u => u.Username == createUserDto1.Username);
		userList.Should().ContainSingle(u => u.Username == createUserDto2.Username);
	}

	[Fact]
	public async Task GetAllAsync_Should_ReturnEmptySuccessResponse()
	{
		//Arrange
		var service = new UserService(_context);

		//Act
		var res = await service.GetAllAsync();

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		var userList = res.Payload as List<UserDto>;
		userList.Should().NotBeNull();
		userList.Should().HaveCount(0);
	}
}
