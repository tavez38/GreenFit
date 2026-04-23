using GreenFit.Serivces;

namespace GreenFit.Pages;

public partial class ChatBotPage : ContentPage
{
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
		
	}
}