using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Models;
using WebChatApi.Infrastructure.Services.EntityServices;
using WebChatApi.UnitTests.Common;

namespace WebChatApi.UnitTests.Tests.User;

public class CreateUserTests : BaseTest
{
	[Fact]
	public async Task CreateUserAsync_Should_ReturnSuccessResponse() 
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

		int userId = 1;

		//Act
		var res = await service.CreateUserAsync(createUserDto);

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		res.Payload.Should().BeNull();
		(await _context.Users.FirstOrDefaultAsync(
			u => u.Id == userId &&
			u.Username == createUserDto.Username &&
			u.FirstName == createUserDto.FirstName &&
			u.LastName == createUserDto.LastName &&
			u.City == createUserDto.City &&
			u.Description == createUserDto.Description &&
			u.Email == createUserDto.Email &&
			u.PhoneNumber == createUserDto.PhoneNumber &&
			u.GitHubUrl == createUserDto.GitHubUrl &&
			u.LinkedInUrl == createUserDto.LinkedInUrl)).Should().NotBeNull();
		_context.Users.Where(
			u => u.Id == userId).Should().HaveCount(1);
	}

	[Fact]
	public async Task CreateUserAsync_Should_ReturnUserAlreadyExists()
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
			Username = "Username",
			FirstName = "FirstName1",
			LastName = "LastName1",
			City = "City1",
			Description = "Description1",
			Email = "Email",
			PhoneNumber = "PhoneNumber1",
			GitHubUrl = "GitHubUrl1",
			LinkedInUrl = "LinkedInUrl1"
		};

		await service.CreateUserAsync(createUserDto1);

		int userId = 1;

		//Act
		var res = await service.CreateUserAsync(createUserDto2);

		//Assert
		res.Success.Should().BeFalse();
		res.Payload.Should().BeNull();
		res.ErrorMessage.Should().BeEquivalentTo(ProblemDetailsResponsesModel.UserAlreadyExists);
		_context.Users.Where(
			u => u.Id == userId).Should().HaveCount(1);
	}
}
