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

	public class BookingService
	{
		public Entities Entities { get; set; }

		public BookingService(Entities entities)
		{
			this.Entities = entities;
		}

		public void Book(BookDTO bookDTO)
		{
			var flight = Entities.Flights.Find(bookDTO.FlightId);
			flight.Book(bookDTO.PassangerEmail, bookDTO.NumberOfSeats);
			Entities.SaveChanges();
		}

		public IEnumerable<BookingRm> FindBookings(Guid flightId)
		{
			return Entities.Flights.Find(flightId).BookingList.Select(booking =>
			new BookingRm(booking.Email, booking.NumberOfSeats));
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