using Application.Test;

namespace Application.Test
{
	public class FlightApplicationSpecifications
	{
		[Theory]
		[InlineData("test@mail.com", 2)]
		[InlineData("hallo@mail.com", 3)]
		public void Books_flights(string passangerEmail, int numberOfSeats)
		{
			var entities = new Entities(new DbContextOptionsBuilder<Entities>()
				.UseInMemoryDatabase("Flights")
				.Options);
			var flight = new Flight(3);
			entities.Flights.Add(flight);

			var bookingService = new BookingService(entities: entities);

			bookingService.Book(new BookDTO(
				flightId: flight.Id, passangerEmail, numberOfSeats
				));

			bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(
				new BookingRm(passangerEmail, numberOfSeats));
		}
	}
}