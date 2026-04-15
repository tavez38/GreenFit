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

        public static Dictionary<string, string> envVariables { get; set; }

        static string appData = FileSystem.AppDataDirectory; // percorso scrivibile per l'app
        static string path = Path.Combine(appData, fileName);

        public static void CreateFileIfNotExists()
        {
            

            if (!File.Exists(path))
            {
                try
                {
                    using (var fs = File.Create(path))
                    {
                        File.WriteAllText(path, envConfigText);
                    }
                }
                catch (IOException ex)
                {
                    // loggare l'errore ma non crashare l'app
                    System.Diagnostics.Debug.WriteLine($"Impossibile creare il file: {ex.Message}");
                }
            }
        }
        public static void readFileText(){
            envVariables = new Dictionary<string, string>();
            string text = File.ReadAllText(path);
            // split text indipendente da terminatore di linea
            string[] lines = text.Split(
            new string []{"\r\n", "\r","\n"}, 
            StringSplitOptions.RemoveEmptyEntries
            );

            foreach (string line in lines)
            {
                string[] parts = line.Split('=');
                if (parts.Length == 2)
                {
                    envVariables.Add(parts[0].Trim(), parts[1].Trim());
                }
            }
        }
    }
}
