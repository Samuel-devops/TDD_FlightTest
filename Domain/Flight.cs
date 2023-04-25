using System.Linq.Expressions;

namespace Domain
{
	public class Flight
	{
		private List<Booking> bookingList = new();

		public IEnumerable<Booking> BookingList
		{ get { return bookingList; } }

		public int RemainingNumberOfSeats { get; set; }

		public Guid Id { get; }

		[Obsolete("Needed by EF")]
		private Flight()
		{ }

		public Flight(int seatCapacity)
		{
			RemainingNumberOfSeats = seatCapacity;
		}

		public object? Book(string passangerEmail, int numberOfSeats)
		{
			if (numberOfSeats > RemainingNumberOfSeats)
			{
				return new OverbookingError();
			}
			RemainingNumberOfSeats -= numberOfSeats;

			bookingList.Add(new Booking(passangerEmail, numberOfSeats));
			return null;
		}

		public object? CancleBooking(string passangerEmail, int numberOfSeatsToCancle)
		{
			if (!bookingList.Any(booking => booking.Email == passangerEmail))
			{
				return new BookingNotFound();
			}

			RemainingNumberOfSeats += numberOfSeatsToCancle;

			return null;
		}
	}
}