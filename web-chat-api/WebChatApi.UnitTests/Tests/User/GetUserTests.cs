using FluentAssertions;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Models;
using WebChatApi.Infrastructure.Services.EntityServices;
using WebChatApi.UnitTests.Common;

namespace WebChatApi.UnitTests.Tests.User;

public class GetUserTests : BaseTest
{
	[Fact]
	public async Task GetByIdAsync_Should_ReturnSuccessResponse()
	{
		//Arrange
		var service = new UserService(_context);

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

		await service.CreateUserAsync(createUserDto);

		int userId = 1;

		//Act
		var res = await service.GetByIdAsync(userId);

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		var user = res.Payload as UserDto;
		user.Should().NotBeNull();
		user.Should().Match<UserDto>(u =>
			u.Id == 1 &&
			u.Username == createUserDto.Username &&
			u.FirstName == createUserDto.FirstName &&
			u.LastName == createUserDto.LastName &&
			u.City == createUserDto.City &&
			u.Description == createUserDto.Description &&
			u.Email == createUserDto.Email &&
			u.PhoneNumber == createUserDto.PhoneNumber &&
			u.GitHubUrl == createUserDto.GitHubUrl &&
			u.LinkedInUrl == createUserDto.LinkedInUrl &&
			u.IsActive == true &&
			u.LastOnline != default
		);

		user.Stack.Should().BeEmpty();
	}

	[Fact]
	public async Task GetByIdAsync_Should_ReturnEntityNotFound()
	{
		//Arrange
		var service = new UserService(_context);

		int userId = 1;

		//Act
		var res = await service.GetByIdAsync(userId);

		//Assert
		res.Success.Should().BeFalse();
		res.Payload.Should().BeNull();
		res.ErrorMessage.Should().BeEquivalentTo(ProblemDetailsResponsesModel.EntityNotFound);
	}
}
