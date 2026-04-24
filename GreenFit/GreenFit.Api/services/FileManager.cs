using static System.Net.Mime.MediaTypeNames;

namespace GreenFit.Api.services
{
    public class FileManager
    {
        public static Dictionary<string, string> keys = new Dictionary<string, string>();

        public static void readEnvKey(){
            keys.Clear();
            string text = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env"));
            string[] lines = text.Split(
           new string[] { "\r\n", "\r", "\n" },
           StringSplitOptions.RemoveEmptyEntries
           );
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || !line.Contains('=')) continue;
                // Limita lo split a 2 parti: chiave e tutto ciò che sta dopo il primo '='
                int separatorIndex = line.IndexOf('=');
                if (separatorIndex > 0)
                {
                    string key = line.Substring(0, separatorIndex).Trim();
                    string value = line.Substring(separatorIndex + 1).Trim();

                    if (!keys.ContainsKey(key))
                        keys.Add(key, value);
                }
            }
        }
    }
}
