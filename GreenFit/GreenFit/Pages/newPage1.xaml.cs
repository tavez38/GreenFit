using GreenFit.Models;
using GreenFit.Serivces;
using Mapsui.Projections;
using Mapsui.UI.Maui;

namespace GreenFit.Pages;

public partial class NewPage1 : ContentPage
{
	private List<PointOfInterest> pointsMap;   

    public NewPage1()
    {
        InitializeComponent();

        // Crea l'oggetto mappa
        var map = new Mapsui.Map();

        // Aggiunge il layer di OpenStreetMap (il layer di base)
        map.Layers.Add(Mapsui.Tiling.OpenStreetMap.CreateTileLayer());

        // Assegna la mappa al controllo XAML
        mappaView.Map = map;

        OnPageLoaded();

        AttivaPosizioneRealTime();

        mappaView.PinClicked += onClickPin;

    }

    private async Task OnPageLoaded()
    {
        await caricaPOIMappa();
        if (pointsMap==null){
                return;
        }
        foreach (var point in pointsMap)
        {
            // Aggiungi un marker per ogni punto di interesse
            var posizione = new Position(point.latitude, point.longitude);
            var pin = new Pin(mappaView)
            {
                Position = posizione,
                Label = point.name,
                Tag = point.id, // Puoi usare l'ID per identificare la palestra quando il pin viene cliccato
                Type = PinType.Pin,
                Color = Microsoft.Maui.Graphics.Color.FromArgb("#FF0000")
            };
            mappaView.Pins.Add(pin);
        }
    }

    public async Task caricaPOIMappa(){
        pointsMap = await ApiServiceGym.GetDatiGeograficiAsync();
        pointsMap.Add(new PointOfInterest(1, 45.4642, 9.1900,"Porva")); //Test marker Milano
    }

	public async void goToAddGymPage(object sender, EventArgs e)
	{
        if (Sessione.sessione.isLoggedIn)
            await Navigation.PushAsync(new CreationGymPage());
        else
        {
            await DisplayAlert("Avviso", "Devi essere loggato per accedere a questa funzionalità!", "Ok");
            return;
        }
        
    }

    private async Task AttivaPosizioneRealTime()
    {
        try
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            var location = await Geolocation.Default.GetLocationAsync(request);

            if (location != null)
            {
                // 1. TRASFORMA LA POSIZIONE PER IL LAYER NATIVO
                // Nota: MyLocationLayer usa Mapsui.UI.Maui.Position
                var myPos = new Mapsui.UI.Maui.Position(location.Latitude, location.Longitude);

                // 2. AGGIORNA IL MARKER NATIVO (il pallino blu si sposterà qui)
                mappaView.MyLocationLayer.UpdateMyLocation(myPos);

                // 3. CENTRA LA MAPPA (solita conversione per il Navigator)
                // In alcune versioni di Mapsui FromLonLat restituisce una ValueTuple<double,double>.
                // Deconstruire e creare esplicitamente un Mapsui.MPoint per evitare l'errore CS1503.
                var (x, y) = Mapsui.Projections.SphericalMercator.FromLonLat(location.Longitude, location.Latitude);
                var mercatorPoint = new Mapsui.MPoint(x, y);

                mappaView.Map.Navigator.CenterOn(mercatorPoint);
                mappaView.Map.Navigator.ZoomToLevel(15);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Errore", "Impossibile aggiornare posizione nativa: " + ex.Message, "OK");
        }
    }

    private async void onClickPin(object sender, PinClickedEventArgs e)
    {
        
        if (e.Pin == null) return;

        
       await Navigation.PushAsync(new GymDeteailsPage(e.Pin.Label, e.Pin.Position,(int) e.Pin.Tag));

    }
}