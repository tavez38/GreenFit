using GreenFit.Models;
using GreenFit.Serivces;
using GreenFit.Shared.Models;
using Mapsui.UI.Maui;

namespace GreenFit.Pages;

public partial class GymDeteailsPage : ContentPage
{
	Position position;
    List<Recensione> recensioneList;
    int idGym;
    string nameGym;
    public GymDeteailsPage(string name, Position posizione, int id)
	{
		InitializeComponent();
        gymNameLabel.Text=name;
        nameGym = name;
        position = posizione;
        idGym = id;
        recensioneList = null;
    }

    //metodo eseguito al caricamento della pagina, recupera le recensioni dal database e le visualizza
    protected override async void OnAppearing(){
        base.OnAppearing();
        recensioneList = await Serivces.ApiServiceRecensioni.GetRecensioniPalestraAsync(idGym);
        recensioneList.Add(new Recensione("Recensione di prova", "Questa è una recensione di prova per testare la visualizzazione.", "djwd", idGym, Valutazioni.BUONA)); //recensione di prova
        IntroRecensioni.Text = "Recensioni (" + recensioneList.Count + ")";
        AggiornaStelleMedia();
        CaricaRecensioni();
        //TODO caricare in database immagini e visualiazare immagine della palestra
    }


    public async void goToChatBot(object sender, EventArgs e)
	{
        if(Sessione.sessione.isLoggedIn)
		    await Navigation.PushAsync(new ChatBotPage(nameGym));
        else{
            await DisplayAlert("Avviso", "Devi essere loggato per accedere a questa funzionalità!", "Ok");
            return;
        }
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

    private void CaricaRecensioni()
    {
        // Puliamo il contenitore per evitare duplicati
        ListaRecensioni.Children.Clear();

        foreach (var rec in recensioneList)
        {
            // 1. Creazione del Frame (Il contenitore esterno)
            var frame = new Frame
            {
                BackgroundColor = Colors.White,
                CornerRadius = 8,
                Padding = 15,
                HasShadow = true,
                BorderColor = Colors.Transparent,
                Margin = new Thickness(0, 5) // Opzionale: spazio tra le schede
            };

            // 2. Creazione del layout verticale interno
            var layoutInterno = new VerticalStackLayout
            {
                Spacing = 5
            };

            // 3. Creazione delle Label (Nome e Testo)
            var labelNome = new Label
            {
                Text = rec.mail +"\t"+ rec.titolo, // Preso dal DB
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromArgb("#333333")
            };

            var labelCommento = new Label
            {
                Text = rec.descrizione, // Preso dal DB
                TextColor = Color.FromArgb("#666666")
            };

            // 4. Assemblaggio dei pezzi (Dall'interno verso l'esterno)
            layoutInterno.Children.Add(labelNome);
            layoutInterno.Children.Add(labelCommento);

            frame.Content = layoutInterno;

            // 5. Inserimento nel documento XAML
            ListaRecensioni.Children.Add(frame);
        }
    }

    private void addRecensione(object sender, EventArgs e)
    {
        if(Sessione.sessione.isLoggedIn == false)
        {
            DisplayAlert("Attenzione", "Devi essere loggato per aggiungere una recensione!", "OK");
            return;
        }

        Navigation.PushAsync(new Scrivirecensione(idGym));
    }

    private void AggiornaStelleMedia()
    {
        if (recensioneList == null || recensioneList.Count == 0)
        {
            MediaVotoLabel.Text = "Nessuna valutazione";
            return;
        }

        // 1. Calcola la media numerica trasformando l'Enum in int
        // Assumendo che il tuo Enum sia (es: PESSIMA=1, MEDIA=3, OTTIMA=5)
        double somma = 0;
        foreach (var rec in recensioneList)
        {
            somma += (int)rec.voto;
        }

        double media = somma / recensioneList.Count;
        int mediaArrotondata = (int)Math.Round(media);

        // 2. Aggiorna il testo della media
        MediaVotoLabel.Text = $"{media:F1} / 5";

        // 3. Colora le stelle
        // StarLayout.Children contiene le 5 Label delle stelle + la Label del testo
        for (int i = 0; i < 5; i++)
        {
            if (StarLayout.Children[i] is Label starLabel)
            {
                // Se l'indice è minore della media, colora di giallo (#FFD700), altrimenti grigio
                starLabel.TextColor = (i < mediaArrotondata) ? Color.FromArgb("#FFD700") : Color.FromArgb("#CCCCCC");
            }
        }
    }
}