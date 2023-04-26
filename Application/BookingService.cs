using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
	public class BookingService
	{
		public Entities Entities { get; set; }

		public BookingService(Entities entities)
		{
			Entities = entities;
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

		public void CancleBooking(CancelBookingDTO cancelBookingDTO)
		{
			var flight = Entities.Flights.Find(cancelBookingDTO.FlightId);
			flight.CancleBooking(cancelBookingDTO.PassangerEmail, cancelBookingDTO.NumberOfSeats);
			Entities.SaveChanges();
		}

		public object GetRemainingNumberOfSeatsFor(Guid id)
		{
			return Entities.Flights.Find(id).RemainingNumberOfSeats;
		}
	}
}