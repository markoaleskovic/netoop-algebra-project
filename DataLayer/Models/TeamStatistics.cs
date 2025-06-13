using System.Collections.Generic;

namespace DataLayer.Models
{
	public class TeamStatistics
	{
		public string? Country { get; set; }
		public int? Attempts_On_Goal { get; set; }
		public int? On_Target { get; set; }
		public int? Off_Target { get; set; }
		public int? Blocked { get; set; }
		public int? Woodwork { get; set; }
		public int? Corners { get; set; }
		public int? Offsides { get; set; }
		public int? Ball_Possession { get; set; }
		public int? Pass_Accuracy { get; set; }
		public int? Num_Passes { get; set; }
		public int? Passes_Completed { get; set; }
		public int? Distance_Covered { get; set; }
		public int? Balls_Recovered { get; set; }
		public int? Tackles { get; set; }
		public int? Clearances { get; set; }
		public int? Yellow_Cards { get; set; }
		public int? Red_Cards { get; set; }
		public int? Fouls_Committed { get; set; }
		public string? Tactics { get; set; }
		public List<Player>? Starting_Eleven { get; set; }
		public List<Player>? Substitutes { get; set; }
	}
}
