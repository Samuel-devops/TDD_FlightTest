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
		public void Remember_Bookings(string passangerEmail, int numberOfSeats)
		{
			var flight = new Flight(3);
			entities.Flights.Add(flight);

			bookingService.Book(new BookDTO(
				flightId: flight.Id, passangerEmail, numberOfSeats
				));

			bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(
				new BookingRm(passangerEmail, numberOfSeats));
		}

		[Theory]
		[InlineData(3, "test@mail.com", 2, 2)]
		[InlineData(6, "first@mail.com", 5, 5)]
		public void Frees_up_seats_after_Booking(int initialCapacity
			, string passangerEmail
			, int numberOfSeatsBook
			, int numberOfSeatsCancels)
		{
			// Given
			var flight = new Flight(initialCapacity);
			entities.Flights.Add(flight);

			bookingService.Book(new BookDTO(
				flightId: flight.Id,
				passangerEmail: passangerEmail,
				numberOfSeats: numberOfSeatsBook
				));

			// When
			bookingService.CancleBooking(new CancelBookingDTO(
				flightId: flight.Id,
				passangerEmail: passangerEmail,
				numberOfSeats: numberOfSeatsCancels
				));

			// Then
			bookingService.GetRemainingNumberOfSeatsFor(flight.Id)
				.Should().Be(initialCapacity);
		}
	}
}