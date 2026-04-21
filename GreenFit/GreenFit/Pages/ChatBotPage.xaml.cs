namespace GreenFit.Pages;

public partial class ChatBotPage : ContentPage
{
	public ChatBotPage()
	{
		InitializeComponent();
	}
	private async void GoToGymDetails(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
    }

	private async void SendAIRequest(object sender, EventArgs e)
	{
		
	}
}