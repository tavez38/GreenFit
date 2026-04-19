namespace GreenFit.Pages;

public partial class NewPage1 : ContentPage
{
	public NewPage1()
	{
		InitializeComponent();
		MapWebView.Source = GreenFit.Serivces.FileManager.envVariables.GetValueOrDefault("URL_MAP");
    }
}