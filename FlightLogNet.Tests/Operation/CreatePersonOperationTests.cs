using FlightLogNet.Integration;
using FlightLogNet.Models;
using FlightLogNet.Operation;
using FlightLogNet.Repositories.Interfaces;
using Moq;
using Xunit;

namespace FlightLogNet.Tests.Operation
{
	public class CreatePersonOperationTests
	{
		private readonly MockRepository mockRepository;

		private readonly Mock<IPersonRepository> mockPersonRepository;
		private readonly Mock<IClubUserDatabase> mockClubUserDatabase;

		public CreatePersonOperationTests()
		{
			mockRepository = new MockRepository(MockBehavior.Strict);

			mockPersonRepository = mockRepository.Create<IPersonRepository>();
			mockClubUserDatabase = mockRepository.Create<IClubUserDatabase>();
		}

		private CreatePersonOperation CreateCreatePersonOperation()
		{
			return new CreatePersonOperation(
				mockPersonRepository.Object,
				mockClubUserDatabase.Object);
		}

		[Fact]
		public void Execute_ShouldReturnNull()
		{
			Assert.True(false);

			// Arrange
			CreatePersonOperation createPersonOperation = CreateCreatePersonOperation();

			// Act
			long? result = createPersonOperation.Execute(null);

			// Assert
			Assert.Null(result);
			mockRepository.VerifyAll();
		}

		[Fact]
		public void Execute_ShouldCreateGuest()
		{
			// Arrange
			CreatePersonOperation createPersonOperation = CreateCreatePersonOperation();
			PersonModel personModel = new PersonModel
			{
				Address = new AddressModel { City = "NY", PostalCode = "456", Street = "2nd Ev", Country = "USA" },
				FirstName = "John",
				LastName = "Smith"
			};
			mockPersonRepository.Setup(repository => repository.AddGuestPerson(personModel)).Returns(10);

			// Act
			long? result = createPersonOperation.Execute(personModel);

			// Assert
			Assert.True(result > 0);
			mockRepository.VerifyAll();
		}

		[Fact]
		public void Execute_ShouldReturnExistingClubMember()
		{
			// Arrange
			CreatePersonOperation createPersonOperation = CreateCreatePersonOperation();
			PersonModel personModel = new PersonModel
			{
				FirstName = "Jan",
				LastName = "Novák",
				MemberId = 3
			};
			long id = 333;
			mockPersonRepository.Setup(repository => repository.TryGetPerson(personModel, out id)).Returns(true);

			// Act
			long? result = createPersonOperation.Execute(personModel);

			// Assert
			Assert.Equal(id, result);
			mockRepository.VerifyAll();
		}

		[Fact]
		public void Execute_ShouldCreateNewClubMember()
		{
			// Arrange
			CreatePersonOperation createPersonOperation = CreateCreatePersonOperation();
			PersonModel personModel = new PersonModel
			{
				FirstName = "Angelina",
				LastName = "Jolie",
				MemberId = 33
			};
			long id = 666;
			mockPersonRepository.Setup(repository => repository.TryGetPerson(personModel, out id)).Returns(false);
			mockPersonRepository.Setup(repository => repository.CreateClubMember(personModel)).Returns(id);
			mockClubUserDatabase.Setup(repository => repository.TryGetClubUser(personModel.MemberId, out personModel)).Returns(true);

			// Act
			long? result = createPersonOperation.Execute(personModel);

			// Assert
			Assert.Equal(id, result);
			mockRepository.VerifyAll();
		}
	}
}
