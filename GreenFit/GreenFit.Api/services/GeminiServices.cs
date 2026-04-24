using System.Text;
using System.Text.Json;

namespace GreenFit.Api.services
{
    public class GeminiServices
    {
        private readonly HttpClient _httpClient;
        private  string ApiKey;
        private  string ApiUrl;

        public GeminiServices()
        {
            _httpClient = new HttpClient();
            ApiKey = FileManager.keys["API_GOOGLE_KEY"];
            ApiUrl = FileManager.keys["API_GOOGLE_URL"];
        }

        public async Task<string> InterrogaGeminiAsync(string prompt)
        {
            try
            {
                // 1. Prepariamo il corpo della richiesta (JSON)
                var requestBody = new
                {
                    contents = new[]
                    {
                        new { parts = new[] { new { text = prompt } } }
                    }
                };

                string jsonPayload = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                // 2. Eseguiamo la chiamata POST aggiungendo la chiave come parametro URL
                var response = await _httpClient.PostAsync($"{ApiUrl}?key={ApiKey}", content);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // 3. Estraiamo il testo della risposta (navigando nel JSON di Google)
                    using var doc = JsonDocument.Parse(jsonResponse);
                    var textResponse = doc.RootElement
                        .GetProperty("candidates")[0]
                        .GetProperty("content")
                        .GetProperty("parts")[0]
                        .GetProperty("text")
                        .GetString();

                    return textResponse ?? "Nessuna risposta ricevuta.";
                }

                return $"Errore API: {response.StatusCode}";
            }
            catch (Exception ex)
            {
                return $"Errore di connessione: {ex.Message}";
            }
        }
    }
}
