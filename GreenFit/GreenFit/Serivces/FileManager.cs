using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection.Metadata;
using Microsoft.Maui.Storage;

namespace GreenFit.Serivces
{
    public static class FileManager
    {
        private const string fileName = ".env";

        private const string envConfigText = "GOOGLE_CLIENT_ID=23526714735-0m5vfobvk91suses2cq2lf1k3pvqfl1a.apps.googleusercontent.com\r\n" +
        "GOOGLE_REDIRECT_URI=com.googleusercontent.apps.23526714735-0m5vfobvk91suses2cq2lf1k3pvqfl1a:/oauth2redirect\r\n" +
        "URL_MAP=https://www.openstreetmap.org/export/embed.html?bbox=8.9,45.4,9.3,45.6&amp;layer=mapnik\r\n" +
        "CONNECTION_STRING_DATABASE=\r\n" +
        "CONNECTION_STRING_OLLAMA=";

        public static Dictionary<string, string> envVariables { get; set; } = new Dictionary<string, string>();

        static string path = Path.Combine(FileSystem.AppDataDirectory, fileName);

        public static async void CreateFileIfNotExists()
        {
            

            if (!File.Exists(path))
            {
                try
                {
                    // Tentativo di recuperare il file dagli Asset (Resources/Raw)
                    using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
                    using var reader = new StreamReader(stream);
                    var content = await reader.ReadToEndAsync();
                    File.WriteAllText(path, content);
                }
                catch (Exception)
                {
                    // Se il file non esiste negli asset, creiamo quello di default
                    File.WriteAllText(path, envConfigText);
                }
            }
            readFileText();
        }
        public static void readFileText(){
            envVariables.Clear();
            if (!File.Exists(path)) return;

            string text = File.ReadAllText(path);

            // split text indipendente da terminatore di linea
            string[] lines = text.Split(
            new string []{"\r\n", "\r","\n"}, 
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

                    if (!envVariables.ContainsKey(key))
                        envVariables.Add(key, value);
                }
            }
        }
    }
}
