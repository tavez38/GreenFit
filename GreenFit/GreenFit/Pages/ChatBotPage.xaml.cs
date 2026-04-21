namespace GreenFit.Pages;

public partial class ChatBotPage : ContentPage
{
	public ChatBotPage()
	{
		InitializeComponent();
	}
	private async void goBackToGymDetails(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
    }
}