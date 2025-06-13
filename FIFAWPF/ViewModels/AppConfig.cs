using System;
using System.IO;

namespace FIFAWPF.ViewModels
{
    public class AppConfig
    {
        private static readonly string ConfigPath = "user_settings.txt";
		public static event Action? ApplicationRefreshed;

		public enum WindowSize
        {
            Small = 0,    // 1024x768
            Medium = 1,   // 1280x720
            Large = 2     // 1920x1080
        }

        public static (int Width, int Height) GetWindowDimensions(WindowSize size)
        {
            return size switch
            {
                WindowSize.Small => (1280, 720),
                WindowSize.Medium => (1600, 900),
                WindowSize.Large => (1920, 1080),
                _ => (1280, 720)
            };
        }

        public static (bool isFullScreen, WindowSize size, string language) Load()
        {
            if (File.Exists(ConfigPath))
            {
                try
                {
                    var lines = File.ReadAllLines(ConfigPath);
                    if (lines.Length >= 3)
                    {
                        return (
                            bool.Parse(lines[0]),
                            (WindowSize)Enum.Parse(typeof(WindowSize), lines[1]),
                            lines[2]
                        );
                    }
                }
                catch
                {
                    // If there's any error reading the file, return default values
                }
            }
            return (false, WindowSize.Medium, "English");
        }

        public static void Save(bool isFullScreen, WindowSize size, string language)
        {
            File.WriteAllLines(ConfigPath, new[] { isFullScreen.ToString(), size.ToString(), language });
        }

        public static void RefreshApplication()
        {

			ApplicationRefreshed?.Invoke();
		}
    }
} 