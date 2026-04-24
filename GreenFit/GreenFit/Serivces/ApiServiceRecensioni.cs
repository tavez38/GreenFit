using GreenFit.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace GreenFit.Serivces
{
    internal class ApiServiceRecensioni
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        // URL base che punta al controller
        private const string BaseUrl = "https://tuo-tunnel-cloudflare.com/api/Recensioni";

        public ApiServiceRecensioni()
        {
            
        }

        // 1. GET: Recupera tutte le recensioni di una specifica palestra
        public static async Task<List<Recensione>> GetRecensioniPalestraAsync(int palestraId)
        {
            try
            {
                // NOTA: Se il tuo controller accetta l'oggetto Gym, 
                // è meglio passare l'ID o modificare il controller per accettare l'ID.
                var response = await _httpClient.GetFromJsonAsync<List<Recensione>>($"{BaseUrl}/RecensioniController/getRecensioni/{palestraId}");
                return response ?? new List<Recensione>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel recupero recensioni: {ex.Message}");
                return new List<Recensione>();
            }
        }

        // 2. POST: Invia una nuova recensione al database
        public static async Task<bool> InviaRecensioneAsync(Recensione nuova)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(BaseUrl, nuova);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nell'invio della recensione: {ex.Message}");
                return false;
            }
        }
    }
}
