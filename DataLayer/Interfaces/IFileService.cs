using System.Collections.Generic;

namespace DataLayer.Interfaces
{
	public interface IFileService
	{
		void SaveToFile<T>(string filePath, List<T> data);
		List<T> LoadFromFile<T>(string filePath);
		void SaveRawJson(string filePath, string json);
		string LoadRawJson(string filePath);
		void DeleteFile(string filePath);
		bool FileExists(string filePath);
	}
}
