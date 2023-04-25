namespace Application.Test
{
	public class FlightApplicationSpecifications
	{
		[Fact]
		public void Books_flights()
		{
			var bookingService = new BookingService();
			bookingService.Book(new BookDTO());
			bookingService.FindBookings().Should().ContainEquivalentOf(
				new BookingRm());
		}
	}

	public class BookingService
	{
		public void Book(BookDTO bookDTO)
		{
		}

		public IEnumerable<BookingRm> FindBookings()
		{
			throw new NotImplementedException();
		}
	}

	public class BookDTO
	{
	}

	public class BookingRm
	{
	}
}