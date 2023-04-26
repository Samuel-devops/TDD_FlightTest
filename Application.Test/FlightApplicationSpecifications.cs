using Application.Test;
using Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Test
{
	public class FlightApplicationSpecifications
	{
		private readonly Entities entities = new Entities(new DbContextOptionsBuilder<Entities>()
				.UseInMemoryDatabase("Flights")
				.Options);

		private readonly BookingService bookingService;

		public FlightApplicationSpecifications()
		{
			bookingService = new BookingService(entities: entities);
		}

		[Theory]
		[InlineData("test@mail.com", 2)]
		[InlineData("hallo@mail.com", 3)]
		public void Books_flights(string passangerEmail, int numberOfSeats)
		{
			var flight = new Flight(3);
			entities.Flights.Add(flight);

			bookingService.Book(new BookDTO(
				flightId: flight.Id, passangerEmail, numberOfSeats
				));

			bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(
				new BookingRm(passangerEmail, numberOfSeats));
		}

		[Fact]
		public void Cancels_Booking()
		{
			// Given
			var flight = new Flight(3);
			entities.Flights.Add(flight);

			bookingService.Book(new BookDTO(
				flightId: flight.Id,
				passangerEmail: "test@mail.com",
				numberOfSeats: 2
				));

			// When
			bookingService.CancleBooking(new CancelBookingDTO(
				flightId: flight.Id,
				passangerEmail: "test@mail.com",
				numberOfSeats: 2
				));

			// Then
			bookingService.GetRemainingNumberOfSeatsFor(flight.Id)
				.Should().Be(3);
		}
	}
}