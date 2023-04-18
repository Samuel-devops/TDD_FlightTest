using System.Linq.Expressions;

namespace Domain
{
	public class Flight
	{
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
			return null;
		}
	}
}