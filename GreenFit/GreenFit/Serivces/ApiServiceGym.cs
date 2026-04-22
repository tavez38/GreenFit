using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using GreenFit.Models;
using GreenFit.Shared.Models;

namespace GreenFit.Serivces
{

    public class ApiServiceGym
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        // Sostituisci con l'indirizzo reale del tuo Cloudflare Tunnel
        private const string BaseUrl = "https://tuo-tunnel-cloudflare.com/api/Gym";

        public ApiServiceGym()
        {
            
        }

        // 1. GET Geografia (Prende solo lat e long e id)
        public static async Task<List<PointOfInterest>> GetDatiGeograficiAsync()
        {
            try
            {
                // Chiama api/Gym/geografia
                var response = await _httpClient.GetFromJsonAsync<List<PointOfInterest>>($"{BaseUrl}/geografia");
                return response ?? new List<PointOfInterest>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore Geografia: {ex.Message}");
                return new List<PointOfInterest>();
            }
        }

        // 2. GET Per Nome (Cerca palestre con un nome specifico)
        public async Task<List<Gym>> CercaPalestraPerNomeAsync(string nome)
        {
            try
            {
                // Chiama api/Gym/cerca/{nome}
                var response = await _httpClient.GetFromJsonAsync<List<Gym>>($"{BaseUrl}/cerca/{nome}");
                return response ?? new List<Gym>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore Ricerca: {ex.Message}");
                return new List<Gym>();
            }
        }

        // 3. POST Crea Palestra (Invia l'oggetto intero al DB)
        public async Task<bool> CreaPalestraAsync(Gym nuovaPalestra)
        {
            try
            {
                // Chiama POST api/Gym
                var response = await _httpClient.PostAsJsonAsync(BaseUrl, nuovaPalestra);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore Creazione: {ex.Message}");
                return false;
            }
        }
    }
}
