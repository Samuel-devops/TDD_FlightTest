namespace FlightTest
{
	public class FlightSpecifications
	{
		[Fact]
		public void Booking_reduces_the_number_of_seats()
		{
			var flight = new Flight(seatCapacity: 3);

			flight.Book("test@email.com", 1);
			flight.RemainingNumberOfSeats.Should().Be(2);
		}

		[Fact]
		public void Avoids_overbooking()
		{
			// Given
			var flight = new Flight(3);

			// When
			var error = flight.Book("test@email.com", 4);

			// Then
			error.Should().BeOfType<OverbookingError>();
		}

		[Fact]
		public void Books_Flights_successfully()
		{
			var flight = new Flight(seatCapacity: 3);
			var error = flight.Book("test@email.com", 1);
			error.Should().BeNull();
		}
	}
}