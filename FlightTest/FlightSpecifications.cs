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

			flight.Book("test@mail.com", numberOfSeats);
			flight.RemainingNumberOfSeats.Should().Be(remainingNumberOfSeats);
		}

		[Fact]
		public void Avoids_overbooking()
		{
			// Given
			var flight = new Flight(3);

			// When
			var error = flight.Book("test@mail.com", 4);

			// Then
			error.Should().BeOfType<OverbookingError>();
		}

		[Fact]
		public void Books_Flights_successfully()
		{
			var flight = new Flight(seatCapacity: 3);
			var error = flight.Book("test@mail.com", 1);
			error.Should().BeNull();
		}

		[Fact]
		public void Remembers_Bookings()
		{
			var flight = new Flight(seatCapacity: 150);

			flight.Book(passangerEmail: "a@b.com", numberOfSeats: 4);
			flight.BookingList.Should().ContainEquivalentOf(new Booking("a@b.com", 4));
		}

		[Theory]
		[InlineData(5, 3, 3, 5)]
		[InlineData(8, 2, 1, 7)]
		[InlineData(24, 6, 4, 22)]
		public void Canceling_Bookings_free_up_Seats(int initialCapacity,
			int numberOfSeatsToBook, int numberOfSeatsToCancle, int result)
		{
			// Given
			var flight = new Flight(initialCapacity);
			flight.Book("test@mail.com", numberOfSeatsToBook);

			// When
			flight.CancleBooking("test@mail.com", numberOfSeatsToCancle);

			// Then
			flight.RemainingNumberOfSeats.Should().Be(result);
		}

		[Theory]
		[InlineData(3, 2)]
		[InlineData(6, 2)]
		public void Doesnot_cancel_bookings_for_passengers_who_have_not_booked(int initialCapacity, int numberOfSeatsToCancle)
		{
			var flight = new Flight(initialCapacity);
			var error = flight.CancleBooking(passangerEmail: "false@mail.com", numberOfSeatsToCancle: numberOfSeatsToCancle);
			error.Should().BeOfType<BookingNotFound>();
		}

		[Theory]
		[InlineData(3, 2, 2, "test@mail.com")]
		[InlineData(6, 4, 2, "hier@mail.com")]
		public void Returns_null_when_successfully_cancle_a_booking(int initialCapacity,
			int numberOfSeatsToBook, int numberOfSeatsToCancle, string email)
		{
			var flight = new Flight(initialCapacity);
			flight.Book(email, numberOfSeatsToBook);
			var error = flight.CancleBooking(passangerEmail: email, numberOfSeatsToCancle: numberOfSeatsToCancle);
			error.Should().BeNull();
		}
	}
}