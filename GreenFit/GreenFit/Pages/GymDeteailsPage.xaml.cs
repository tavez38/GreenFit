using Mapsui.UI.Maui;

namespace GreenFit.Pages;

public partial class GymDeteailsPage : ContentPage
{
	Position position;
    public GymDeteailsPage(string name, Position posizione)
	{
		InitializeComponent();
		gymNameLabel.Text = name;
		position = posizione;
    }

	public async void goToChatBot(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ChatBotPage());
    }

	private async void openMaps(object sender, EventArgs e)
	{
        string lat = position.Latitude.ToString().Replace(",",".");
        string lon = position.Longitude.ToString().Replace(",",".");

        // URL universale di Google Maps per le indicazioni (Directions)
        // daddr = destination address
        string url = $"https://www.google.com/maps/dir/?api=1&destination={lat},{lon}";

        await Launcher.Default.OpenAsync(url);
    }
}