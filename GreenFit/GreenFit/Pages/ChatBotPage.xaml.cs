using GreenFit.Serivces;
using System.Net.Http.Json;
using System.Text.Json;

namespace GreenFit.Pages;

public partial class ChatBotPage : ContentPage
{
	HttpClient client;
	public ChatBotPage( string name)
	{
		InitializeComponent();
		NameGym.Text = name;
		responseBot.Text =$"Ciao {Sessione.sessione.nome} {Sessione.sessione.cognome}, descrivimi che tipo di allenamento avevi in mente ";

    }
	private async void GoToGymDetails(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
    }

	private async void SendAIRequest(object sender, EventArgs e)
	{
		try
		{
			client = new HttpClient();
			var requestt = Prompt.Text;
			if (string.IsNullOrEmpty(requestt))
			{
				await DisplayAlert("Errore", "Per favore, inserisci una richiesta valida.", "OK");
				return;
			}
			var payload = new { request = requestt };
			string json = System.Text.Json.JsonSerializer.Serialize(payload);
			var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
			// Specificare il tipo generico e attendere il Task restituito
			var response = await client.PostAsync("/AIController/AiRequest", content);
			if (response.IsSuccessStatusCode)
			{
				string jsonResult = await response.Content.ReadAsStringAsync();
				using var doc = JsonDocument.Parse(jsonResult);
				Prompt.Text = doc.RootElement.GetProperty("ris").GetString();
			}

			Prompt.Text = $"Errore server: {response.StatusCode}";
		}
		catch (Exception ex)
		{
			DisplayAlert("Errore", $"Si è verificato un errore: {ex.Message}", "OK");

		}
	}
}