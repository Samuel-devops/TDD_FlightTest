using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
	public class BookDTO
	{
		public Guid FlightId { get; set; }
		public string PassangerEmail { get; set; }
		public int NumberOfSeats { get; set; }

		public BookDTO(Guid flightId, string passangerEmail, int numberOfSeats)
		{
			FlightId = flightId;
			PassangerEmail = passangerEmail;
			NumberOfSeats = numberOfSeats;
		}
	}
}