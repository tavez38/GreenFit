using GreenFit.Serivces;
using GreenFit.Shared.Models;

namespace GreenFit.Pages;

public partial class Scrivirecensione : ContentPage
{
	private int idGym;
    public Scrivirecensione(int idGym)
	{
		InitializeComponent();
		this.idGym = idGym;
        PickerVoto.ItemsSource = Enum.GetValues(typeof(Valutazioni));
    }

    private async void OnInviaRecensioneClicked(object sender, EventArgs e)
    {
        // 1. Validazione base
        if (string.IsNullOrWhiteSpace(EntryTitolo.Text) || PickerVoto.SelectedItem == null)
        {
            await DisplayAlert("Attenzione", "Inserisci almeno un titolo e una valutazione", "OK");
            return;
        }

        // 2. Recupero dati
        string titolo = EntryTitolo.Text;
        string descrizione = EditorDescrizione.Text;
        Valutazioni votoSelezionato = (Valutazioni)PickerVoto.SelectedItem;

        await ApiServiceRecensioni.InviaRecensioneAsync(new Recensione(titolo,descrizione,Sessione.sessione.email,idGym,votoSelezionato));
        await DisplayAlert("Recensione Inviata", "La tua recensione è stata inviata con successo!", "OK");
        Navigation.PopAsync();
    }
}