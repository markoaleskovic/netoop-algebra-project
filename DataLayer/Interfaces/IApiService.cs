using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Models;

namespace DataLayer.Interfaces
{
	public interface IApiService
	{
		Task<List<Team>> GetTeamsAsync(string gender);
		Task<List<Team>> GetTeamsResultsAsync(string gender);
		Task<List<GroupResult>> GetTeamsGroupResultsAsync(string gender);
		Task<List<Match>> GetMatchesAsync(string gender);
		Task<List<Match>> GetMatchesByCountryAsync(string gender, string fifaCode);
	}
}
