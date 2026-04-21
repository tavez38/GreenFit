using System;
using System.Collections.Generic;
using System.Text;
using Mscc.GenerativeAI;
using GreenFit.Serivces;
using Mscc.GenerativeAI.Types;

namespace GreenFit.Serivces
{
    public class AIServices
    {
        private readonly GenerativeModel _model;

        public AIServices()
        {
            // Recupera la chiave dal dizionario .env caricato all'avvio
            if (FileManager.envVariables.TryGetValue("API_GEMINI", out var apiKey))
            {
                // Inizializza l'oggetto Google AI
                var googleAI = new GoogleAI(apiKey);
                // Utilizziamo il modello Flash: è gratis, veloce e ottimo per testi brevi
                _model = googleAI.GenerativeModel(Model.Gemini25Flash);
            }
        }
        public async Task<string> GeneraScheda(List<string> attrezzi, string obiettivo)
        {
            if (_model == null) return "Errore: API Key non configurata.";

            // Costruiamo il prompt per l'AI
            string prompt = $"Sei un personal trainer esperto. " +
                            $"Crea una scheda di allenamento per l'obiettivo: {obiettivo}. " +
                            $"Attrezzi disponibili: {string.Join(", ", attrezzi)}. " +
                            $"Rispondi in italiano con un elenco puntato chiaro.";

            try
            {
                var response = await _model.GenerateContent(prompt);
                return response.Text;
            }
            catch (Exception ex)
            {
                return $"Errore durante la generazione: {ex.Message}";
            }
        }
    }
}

