using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Models;
using WebChatApi.Infrastructure.Services.EntityServices;
using WebChatApi.UnitTests.Common;

namespace WebChatApi.UnitTests.Tests.User;

public class UpdateUserTests : BaseTest
{
	[Fact]
	public async Task UpdateUserAsync_Should_ReturnSuccessResponse()
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

		var updateUserDto = new UpdateUserDto
		{
			Id = userId,
			FirstName = "FirstName1",
			LastName = "LastName1",
			City = "City1",
			Description = "Description1",
			Email = "Email1",
			PhoneNumber = "PhoneNumber1",
			GitHubUrl = "GitHubUrl1",
			LinkedInUrl = "LinkedInUrl1"
		};
		//Act
		var res = await service.UpdateUserAsync(updateUserDto);

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		res.Payload.Should().BeNull();
		(await _context.Users.FirstOrDefaultAsync(
			u => u.Id == userId &&
			u.FirstName == updateUserDto.FirstName &&
			u.LastName == updateUserDto.LastName &&
			u.City == updateUserDto.City &&
			u.Description == updateUserDto.Description &&
			u.Email == updateUserDto.Email &&
			u.PhoneNumber == updateUserDto.PhoneNumber &&
			u.GitHubUrl == updateUserDto.GitHubUrl &&
			u.LinkedInUrl == updateUserDto.LinkedInUrl)).Should().NotBeNull();
		_context.Users.Where(
			u => u.Id == userId).Should().HaveCount(1);
	}

	[Fact]
	public async Task UpdateUserAsync_Should_ReturnUserNotFound()
	{
		//Arrange
		var service = new UserService(_context);
		int userId = 1;

		var updateUserDto = new UpdateUserDto
		{
			Id = userId,
			FirstName = "FirstName",
			LastName = "LastName",
			City = "City",
			Description = "Description",
			Email = "Email",
			PhoneNumber = "PhoneNumber",
			GitHubUrl = "GitHubUrl",
			LinkedInUrl = "LinkedInUrl"
		};

		//Act
		var res = await service.UpdateUserAsync(updateUserDto);

		//Assert
		res.Success.Should().BeFalse();
		res.Payload.Should().BeNull();
		res.ErrorMessage.Should().BeEquivalentTo(ProblemDetailsResponsesModel.UserNotFound);
		(await _context.Users.FirstOrDefaultAsync(
			u => u.Id == userId)).Should().BeNull();
	}
}
