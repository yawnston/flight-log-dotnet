using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FlightLogNet.Models;
using FlightLogNet.Repositories;
using FlightLogNet.Repositories.Interfaces;
using Xunit;

namespace FlightLogNet.Tests.Repositories
{
	public class FlightRepositoryTests
	{
		private readonly IMapper mapper;

		public FlightRepositoryTests(IMapper mapper)
		{
			this.mapper = mapper;
		}

		private IFlightRepository CreateFlightRepository()
		{
			return new FlightRepository(mapper);
		}

		private void RenewDatabase()
		{
			DatabaseGenerator.CreateTestDatabase();
		}

		[Fact]
		public void GetFlightsOfTypeGlider_Return2Gliders()
		{
			// Arrange
			RenewDatabase();
			var flightRepository = CreateFlightRepository();

			// Act
			var result = flightRepository.GetAllFlights(FlightType.Glider);

			// Assert
			Assert.True(result.Count == 2, "In test database is 2 gliders.");
		}

		[Fact]
		public void GetAirplanesInAir_ReturnFlightModels()
		{
			// Arrange
			RenewDatabase();
			var flightRepository = CreateFlightRepository();

			// Act
			IList<FlightModel> result = flightRepository.GetAirplanesInAir();

			// Assert
			Assert.NotEmpty(result);
		}

		[Fact]
		public void GetReport_StateUnderTest_ExpectedBehavior()
		{
			// Arrange
			RenewDatabase();
			var flightRepository = CreateFlightRepository();

			// Act
			var result = flightRepository.GetReport();
			var flights = result.SelectMany(model => new[] { model.Glider, model.Towplane }).ToList();

			// Assert
			Assert.True(result.Count == 3, "In test database is 3 flight starts");
			Assert.True(flights[4] == null, "Last flight start should have null glider.");
		}
	}
}
