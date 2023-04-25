using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
	public class BookingRm
	{
		public string PassangerEmail { get; set; }
		public int NumberOfSeats { get; set; }

		public BookingRm(string passangerEmail, int numberOfSeats)
		{
			PassangerEmail = passangerEmail;
			NumberOfSeats = numberOfSeats;
		}
	}
}