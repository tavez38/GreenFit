namespace GreenFit.Pages;

public partial class NewPage1 : ContentPage
{
	public NewPage1()
	{
		InitializeComponent();
		MapWebView.Source = GreenFit.Serivces.FileManager.envVariables.GetValueOrDefault("URL_MAP", 
		"https://www.openstreetmap.org/export/embed.html?bbox=8.9,45.4,9.3,45.6&amp;layer=mapnik");
    }
}