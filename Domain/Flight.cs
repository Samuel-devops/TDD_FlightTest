using System.Linq.Expressions;

namespace Domain
{
	public class Flight
	{
		private List<Booking> bookingList = new();

		public IEnumerable<Booking> BookingList
		{ get { return bookingList; } }

		public int RemainingNumberOfSeats { get; set; }

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
			var test = bookingList.Any(booking => booking.Email == passangerEmail);
			if (!bookingList.Any(booking => booking.Email == passangerEmail))
			{
				return new BookingNotFound();
			}

			RemainingNumberOfSeats += numberOfSeatsToCancle;

			return null;
		}
	}
}