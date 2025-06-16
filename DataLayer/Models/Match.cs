using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
	public class Match
	{
		public string? Venue { get; set; }
		public string? Location { get; set; }
		public string? Status { get; set; }
		public string? Time { get; set; }
		public string? Fifa_Id { get; set; }
		public Weather? Weather { get; set; }
		public string? Attendance { get; set; }
		public List<string>? Officials { get; set; }
		public string? Stage_Name { get; set; }
		public string? Home_Team_Country { get; set; }
		public string? Away_Team_Country { get; set; }
		public DateTime? Datetime { get; set; }
		public string? Winner { get; set; }
		public string? Winner_Code { get; set; }
		public TeamScore? Home_Team { get; set; }
		public TeamScore? Away_Team { get; set; }
		public List<Event>? Home_Team_Events { get; set; }
		public List<Event>? Away_Team_Events { get; set; }
		public TeamStatistics? Home_Team_Statistics { get; set; }
		public TeamStatistics? Away_Team_Statistics { get; set; }
		public DateTime? Last_Event_Update_At { get; set; }
		public DateTime? Last_Score_Update_At { get; set; }
	}

	public class TeamScore
	{
		public string? Country { get; set; }
		public string? Code { get; set; }
		public int? Goals { get; set; }
		public int? Penalties { get; set; }
	}
}
