using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
	public class MatchScore
	{
		public Team? LeftTeam { get; set; }
		public Team? RightTeam { get; set; }
		public int? LeftScore { get; set; }
		public int? RightScore { get; set; }
	}
}
