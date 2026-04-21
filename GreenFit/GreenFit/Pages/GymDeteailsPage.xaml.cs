namespace GreenFit.Pages;

public partial class GymDeteailsPage : ContentPage
{
	public GymDeteailsPage()
	{
		InitializeComponent();
	}

	public async void goToChatBot(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ChatBotPage());
    }
}