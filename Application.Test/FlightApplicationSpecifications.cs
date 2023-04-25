namespace Application.Test
{
	public class FlightApplicationSpecifications
	{
		[Fact]
		public void Books_flights()
		{
			var entities = new Entities(new DbContextOptionsBuilder<Entities>()
				.UseInMemoryDatabase("Flights")
				.Options);
			var flight = new Flight(3);
			entities.Flights.Add(flight);

			var bookingService = new BookingService(entities: entities);

			bookingService.Book(new BookDTO(
				flightId: flight.Id, passangerEmail: "test@mail.com", numberOfSeats: 2
				));

			bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(
				new BookingRm(passangerEmail: "test@mail.com", numberOfSeats: 2));
		}
	}

	public class BookingService
	{
		public BookingService(Entities entities)
		{
		}

		public void Book(BookDTO bookDTO)
		{
		}

		public IEnumerable<BookingRm> FindBookings(Guid flightId)
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