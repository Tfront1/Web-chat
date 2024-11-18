using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Models;
using WebChatApi.Infrastructure.Services.EntityServices;
using WebChatApi.UnitTests.Common;

namespace WebChatApi.UnitTests.Tests.User;

public class DeleteUserTests : BaseTest
{
	[Fact]
	public async Task DeleteAsync_Should_ReturnSuccessResponse()
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

		//Act
		var res = await service.DeleteAsync(1);

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		res.Payload.Should().BeNull();
		(await _context.Users.FirstOrDefaultAsync(
			u => u.Id == 1)).Should().BeNull();
	}

	[Fact]
	public async Task DeleteAsync_Should_ReturnEntityNotFound()
	{
		//Arrange
		var service = new UserService(_context);
		int userId = 1;

		//Act
		var res = await service.DeleteAsync(userId);

		//Assert
		res.Success.Should().BeFalse();
		res.Payload.Should().BeNull();
		res.ErrorMessage.Should().BeEquivalentTo(ProblemDetailsResponsesModel.EntityNotFound);
		(await _context.Users.FirstOrDefaultAsync(
			u => u.Id == userId)).Should().BeNull();
	}
}
