using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using DataLayer.Interfaces;

namespace DataLayer.Services
{
	// Handles file I/O and JSON (de)serialization for caching/offline data
	public class FileService : IFileService
	{
		public void SaveToFile<T>(string filePath, List<T> data)
		{
			try
			{
				var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
				File.WriteAllText(filePath, json);
			}
			catch (UnauthorizedAccessException ex)
			{
				LogError(ex);
				throw new ApplicationException("No permission to write to file.");
			}
			catch (IOException ex)
			{
				LogError(ex);
				throw new ApplicationException("Error writing to file.");
			}
			catch (Exception ex)
			{
				LogError(ex);
				throw;
			}
		}

		public List<T> LoadFromFile<T>(string filePath)
		{
			try
			{
				if (!File.Exists(filePath))
					return new List<T>();
				var json = File.ReadAllText(filePath);
				return JsonSerializer.Deserialize<List<T>>(json);
			}
			catch (FileNotFoundException ex)
			{
				LogError(ex);
				throw new ApplicationException("File not found.");
			}
			catch (IOException ex)
			{
				LogError(ex);
				throw new ApplicationException("Error reading from file.");
			}
			catch (Exception ex)
			{
				LogError(ex);
				throw;
			}
		}

		public void DeleteFile(string filePath)
		{
			try
			{
				if (File.Exists(filePath))
					File.Delete(filePath);
			}
			catch (Exception ex)
			{
				LogError(ex);
				throw new ApplicationException("Error deleting file.");
			}
		}

		public bool FileExists(string filePath)
		{
			try
			{
				return File.Exists(filePath);
			}
			catch (Exception ex)
			{
				LogError(ex);
				throw new ApplicationException("Error checking file existence.");
			}
		}

		public void SaveRawJson(string filePath, string json)
		{
			File.WriteAllText(filePath, json);
		}

		public string LoadRawJson(string filePath)
		{
			return File.Exists(filePath) ? File.ReadAllText(filePath) : string.Empty;
		}

		public static void LogError(Exception ex)
		{
			File.AppendAllText("error.log", $"{DateTime.Now}: {ex}\n");
		}
	}
}
