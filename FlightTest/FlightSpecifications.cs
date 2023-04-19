namespace FlightTest
{
	public class FlightSpecifications
	{
		[Theory]
		[InlineData(3, 1, 2)]
		[InlineData(6, 3, 3)]
		[InlineData(10, 4, 6)]
		public void Booking_reduces_the_number_of_seats(int seatCapacity, int numberOfSeats, int remainingNumberOfSeats)
		{
			var flight = new Flight(seatCapacity: seatCapacity);

			flight.Book("test@email.com", numberOfSeats);
			flight.RemainingNumberOfSeats.Should().Be(remainingNumberOfSeats);
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