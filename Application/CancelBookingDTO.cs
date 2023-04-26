using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
	public class CancelBookingDTO
	{
		public Guid FlightId { get; set; }
		public string PassangerEmail { get; set; }
		public int NumberOfSeats { get; set; }

		public CancelBookingDTO(Guid flightId, string passangerEmail, int numberOfSeats)
		{
			this.FlightId = flightId;
			this.PassangerEmail = passangerEmail;
			this.NumberOfSeats = numberOfSeats;
		}
	}
}