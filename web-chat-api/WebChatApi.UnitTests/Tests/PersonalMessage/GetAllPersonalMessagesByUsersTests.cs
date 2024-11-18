using FluentAssertions;
using WebChatApi.Contracts.Dtos.PersonalMessage;
using WebChatApi.Contracts.Dtos.User;
using WebChatApi.Contracts.Models;
using WebChatApi.Infrastructure.Services.EntityServices;
using WebChatApi.UnitTests.Common;

namespace WebChatApi.UnitTests.Tests.PersonalMessage;

public class GetAllPersonalMessagesByUsersTests : BaseTest
{
	[Fact]
	public async Task GetAllPersonalMessagesByUsersAsync_Should_ReturnSuccessResponse()
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

		var service = new PersonalMessageService(_context);

		var createPersonalMessageDto1 = new CreatePersonalMessageDto
		{
			AuthorId = user1Id,
			RecipientId = user2Id,
			Content = "Content1"
		};
		var createPersonalMessageDto2 = new CreatePersonalMessageDto
		{
			AuthorId = user1Id,
			RecipientId = user2Id,
			Content = "Content2"
		};

		await service.CreatePersonalMessageAsync(createPersonalMessageDto1);
		await service.CreatePersonalMessageAsync(createPersonalMessageDto2);

		int personalMessage1Id = 1;
		int personalMessage2Id = 2;

		var getPersonalMessagesByUsersDto = new GetPersonalMessagesByUsersDto
		{
			AuthorId = user1Id,
			RecipientId = user2Id,
		};

		//Act
		var res = await service.GetAllPersonalMessagesByUsersAsync(getPersonalMessagesByUsersDto);

		//Assert
		res.Success.Should().BeTrue();
		res.ErrorMessage.Should().BeNullOrEmpty();
		var personalMessageList = res.Payload as List<PersonalMessageDto>;
		personalMessageList.Should().NotBeNull();
		personalMessageList.Should().HaveCount(2);
		personalMessageList.Should().ContainSingle(u => u.Id == personalMessage1Id);
		personalMessageList.Should().ContainSingle(u => u.Id == personalMessage2Id);
	}

	[Fact]
	public async Task GetAllPersonalMessagesByUsersAsync_Should_ReturnAuthorNotFound()
	{
		//Arrange
		int user1Id = 1;

		var service = new PersonalMessageService(_context);

		var getPersonalMessagesByUsersDto = new GetPersonalMessagesByUsersDto
		{
			AuthorId = user1Id,
			RecipientId = user1Id,
		};

		//Act
		var res = await service.GetAllPersonalMessagesByUsersAsync(getPersonalMessagesByUsersDto);

		//Assert
		res.Success.Should().BeFalse();
		res.Payload.Should().BeNull();
		res.ErrorMessage.Should().BeEquivalentTo(ProblemDetailsResponsesModel.AuthorNotFound);
	}

	[Fact]
	public async Task GetAllPersonalMessagesByUsersAsync_Should_ReturnRecipientNotFound()
	{
		//Arrange
		var userService = new UserService(_context);

		var createUserDto = new CreateUserDto
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
		
		await userService.CreateUserAsync(createUserDto);

		int user1Id = 1;
		int user2Id = 2;

		var service = new PersonalMessageService(_context);

		var createPersonalMessageDto = new CreatePersonalMessageDto
		{
			AuthorId = user1Id,
			RecipientId = user2Id,
			Content = "Content1"
		};

		await service.CreatePersonalMessageAsync(createPersonalMessageDto);

		int personalMessage1Id = 1;

		var getPersonalMessagesByUsersDto = new GetPersonalMessagesByUsersDto
		{
			AuthorId = user1Id,
			RecipientId = user2Id,
		};

		//Act
		var res = await service.GetAllPersonalMessagesByUsersAsync(getPersonalMessagesByUsersDto);

		//Assert
		res.Success.Should().BeFalse();
		res.Payload.Should().BeNull();
		res.ErrorMessage.Should().BeEquivalentTo(ProblemDetailsResponsesModel.RecipientNotFound);
	}
}
