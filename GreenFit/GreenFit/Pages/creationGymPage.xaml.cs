using CommunityToolkit.Maui.Extensions;
using GreenFit.Models;

namespace GreenFit.Pages;

public partial class CreationGymPage : ContentPage
{
    string pathImg = string.Empty;
    public CreationGymPage()
	{
        InitializeComponent();
        getCurrentLocation();
    }
    public void goBackToMap(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
    public void addGym(object sender, EventArgs e)
    {
        string name = GymNameEntry.Text;
        string description = GymDescEntry.Text;
        string coordinate = GymCoordinateEntry.Text;
        string latitude = coordinate.Split(',')[0].Trim();
        string longitude = coordinate.Split(',')[1].Trim();

        DisplayAlert("Palestra Aggiunta", $"Nome: {name}\nDescrizione: {description}\n", "OK");
        //TODO implementare invio dati al db
    }
    public void resetAddGym(object sender, EventArgs e){
        GymNameEntry.Text = string.Empty;
        GymDescEntry.Text = string.Empty;
        GymCoordinateEntry.Text = string.Empty;
        ImgAnteprima.Source = null;
        ImgAnteprima.IsVisible = false;
        GymImgDefaultText.IsVisible = true;
    }
    public async void addImageGym(object sender, EventArgs e)
    {
        var photo = await MediaPicker.Default.PickPhotoAsync();

        if (photo != null)
        {
            // Ottieni lo stream dell'immagine selezionata
            var stream = await photo.OpenReadAsync();
            //imposta l'immagine selezionata come fonte per l'elemento Image
            ImgAnteprima.Source = ImageSource.FromStream(() => stream);
            //gestione della visibilità scritta default e immagine di anteprima
            ImgAnteprima.IsVisible = true;
            GymImgDefaultText.IsVisible = false;

            //salvataggio path per fututro invio a db
            pathImg = photo.FullPath;

        }
    }
    public async void getCurrentLocation() {
        try {
            //controllo del permesso di localizzazione dato in precedenza, se non è stato dato viene richiesto
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>(); 
            if (status != PermissionStatus.Granted) {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }
            if (status != PermissionStatus.Granted) {
                await DisplayAlert("Permesso negato", "Impossibile ottenere la posizione senza permessi; perfavore inserire la posizione della palestra a mano.", "OK");
                return;
            }

            // 2. Configura la richiesta di posizione
            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

            // 3. Ottieni la posizione
            Location location = await Geolocation.Default.GetLocationAsync(request);

            if (location != null)
            {
                // 4. Inserisci i dati nella Entry
                GymCoordinateEntry.Text = location.Latitude.ToString() + ", " + location.Longitude.ToString();

                await DisplayAlert("Successo", "Coordinate inserite correttamente", "OK");
            }
        }
        catch (FeatureNotSupportedException)
        {
            await DisplayAlert("Errore", "Il GPS non è supportato su questo dispositivo", "OK");
        }
        catch (PermissionException)
        {
            await DisplayAlert("Errore", "L'app non ha i permessi di localizzazione", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Errore", $"Impossibile ottenere la posizione: {ex.Message}", "OK");
        }
    }
    private async void OpenPopUpClicked(object sender, EventArgs e)
    {
        var popup = new PopUpAttrezzi();

        // Mostra il popup e attendi il risultato
        await this.ShowPopupAsync(popup);

        if (PopUpAttrezzi.SelectedItemsResult!=null)
        {
           
            // Uniamo i nomi degli attrezzi in una stringa separata da virgole
            LblAttrezziScelti.Text = string.Join(", ", PopUpAttrezzi.SelectedItemsResult);

            // Ora hai la lista 'scelti' pronta per essere salvata nel DB
        }
    }
}