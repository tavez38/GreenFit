using GreenFit.Models;
using System.Text.Json;

namespace GreenFit
{
    public partial class MainPage : ContentPage
    {
       // int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }
        private async void OnGoogleLoginTapped(object sender, EventArgs e)
        {
            string email;
            string nome;
            string cognome;
            try
            {
                // 1. Configura i parametri di Google
                string clientId = Serivces.FileManager.envVariables["GOOGLE_CLIENT_ID"];
                string redirectUri = Serivces.FileManager.envVariables["GOOGLE_REDIRECT_URI"];

                // 2. Costruisci l'URL di autorizzazione di Google

                string authUrl = $"https://accounts.google.com/o/oauth2/v2/auth?" +
                 $"client_id={clientId}&" +
                 $"response_type=code&" +
                 $"scope=openid%20email%20profile&" +
                 $"redirect_uri={Uri.EscapeDataString(redirectUri)}";

                // 3. Avvia l'autenticazione tramite browser di sistema
                WebAuthenticatorResult result = await WebAuthenticator.Default.AuthenticateAsync(
                    new Uri(authUrl),
                    new Uri(redirectUri));

                if (result != null)
                {
                    if (result?.Properties.ContainsKey("code") == true)
                    {
                        string authCode = result.Properties["code"];

                        // A questo punto abbiamo il CODE, ma non abbiamo ancora i dati dell'utente motivo per cui dobbiamo fare un ulteriore passaggio 
                        // Usiamo una HttpClient per chiedere a Google i dati dell'utente
                        using var client = new HttpClient();

                        // 1. Scambiamo il CODE con un ACCESS_TOKEN
                        //prepariamo i dati da inviare a Google per ottenere l'access_token
                        var content = new FormUrlEncodedContent(new Dictionary<string, string>
                        {
                            { "code", authCode },
                            { "client_id", clientId },
                            { "redirect_uri", redirectUri },
                            { "grant_type", "authorization_code" }
                        });

                        var response = await client.PostAsync("https://oauth2.googleapis.com/token", content);
                        // Leggiamo come stringa la risposta di Google che conterrà l'access_token
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var doc = JsonDocument.Parse(jsonResponse);
                        if (doc.RootElement.TryGetProperty("access_token", out var accessTokenElement))
                        {
                            string accessToken = accessTokenElement.GetString();

                            // 2. Ora usiamo l'access_token per chiamare l'endpoint "userinfo" di Google
                            // Questo endpoint restituisce Nome, Cognome, Email e Foto in chiaro
                            var userInfoResponse = await client.GetAsync($"https://www.googleapis.com/oauth2/v2/userinfo?access_token={accessToken}");
                            var userInfoJson = await userInfoResponse.Content.ReadAsStringAsync();

                            // 3. Estraiamo finalmente i dati finali
                            using var userDoc = JsonDocument.Parse(userInfoJson);
                            var root = userDoc.RootElement;

                            email = root.GetProperty("email").GetString();
                            nome = root.GetProperty("given_name").GetString();
                            cognome = root.GetProperty("family_name").GetString();
                            

                            // Prova a visualizzarli!
                            await DisplayAlert("Login Successo", $"Bentornato {nome} {cognome}!", "OK");

                            Utente utente = new Utente(true);
                            utente.nome = nome;
                            utente.cognome = cognome;
                            utente.email = email;
                            Sessione.utente = utente;



                            // Ora puoi andare alla mappa
                            await Shell.Current.GoToAsync("MapPage");
                        }
                        else
                        {
                            await DisplayAlert("Errore", "Impossibile scambiare il codice con il token.", "OK");
                        }
                    }
                }

                    

            }
            catch (TaskCanceledException)
            {
                // L'utente ha chiuso la finestra senza fare nulla
            }
            catch (Exception ex)
            {
                await DisplayAlert("Errore Auth", ex.Message, "OK");
            }
        }

        private async void OnGuestLoginTapped(object sender, EventArgs e)
        {
            Utente u = new Utente(false);
            await Shell.Current.GoToAsync("MapPage");
        }


    }
}
