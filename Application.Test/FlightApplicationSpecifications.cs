namespace Application.Test
{
	public class FlightApplicationSpecifications
	{
		[Fact]
		public void Books_flights()
		{
			var bookingService = new BookingService();

			bookingService.Book(new BookDTO(
				flightId: Guid.NewGuid(), passangerEmail: "test@mail.com", numberOfSeats: 2
				));

			bookingService.FindBookings().Should().ContainEquivalentOf(
				new BookingRm(passangerEmail: "test@mail.com", numberOfSeats: 2));
		}
	}

	public class BookingService
	{
		public void Book(BookDTO bookDTO)
		{
		}

		public IEnumerable<BookingRm> FindBookings()
		{
			return new[]
			{
				new BookingRm(passangerEmail: "test@mail.com", numberOfSeats: 2)
			};
		}
	}

	public class BookDTO
	{
		public Guid FlightId { get; set; }
		public string PassangerEmail { get; set; }
		public int NumberOfSeats { get; set; }

		public BookDTO(Guid flightId, string passangerEmail, int numberOfSeats)
		{
			this.FlightId = flightId;
			this.PassangerEmail = passangerEmail;
			this.NumberOfSeats = numberOfSeats;
		}
	}

	public class BookingRm
	{
		public string PassangerEmail { get; set; }
		public int NumberOfSeats { get; set; }

		public BookingRm(string passangerEmail, int numberOfSeats)
		{
			this.PassangerEmail = passangerEmail;
			this.NumberOfSeats = numberOfSeats;
		}
	}
}