using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
	public class MatchAttendance
	{
		public string? Location { get; set; }
		public int? Attendance { get; set; }
		public string? Home_Team { get; set; }
		public string? Away_Team { get; set; }
	}
}
