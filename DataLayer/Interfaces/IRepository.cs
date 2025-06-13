using System.Threading.Tasks;
using DataLayer.Models;

namespace DataLayer.Interfaces
{
	public interface IRepository
	{
		// API
		Task<List<Team>> GetTeamsAsync(string gender, bool useCache = false);
		Task<List<Team>> GetTeamsResultsAsync(string gender, bool useCache = false);
		Task<List<GroupResult>> GetTeamsGroupResultsAsync(string gender, bool useCache = false);
		Task<List<Match>> GetMatchesAsync(string gender, bool useCache = false);
		Task<List<Match>> GetMatchesByCountryAsync(string gender, string fifaCode, bool useCache = false);

		// File
		void SaveData<T>(string filePath, List<T> data);
		List<T> LoadData<T>(string filePath);
	}
}
