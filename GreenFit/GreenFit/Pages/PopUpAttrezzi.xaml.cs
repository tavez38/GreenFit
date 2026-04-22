using CommunityToolkit.Maui.Views;
using GreenFit.Models;
using GreenFit.Shared.Models;
namespace GreenFit.Pages;

public partial class PopUpAttrezzi : Popup
{
    // Property to expose selected items to the caller after the popup is closed
    public static List<AttrezziPalestre>? SelectedItemsResult { get; private set; }

	public PopUpAttrezzi()
	{
		InitializeComponent();
        ListaAttrezzi.ItemsSource = Enum.GetValues(typeof(AttrezziPalestre));
    }
    public async void OnConfirmClicked(object sender, EventArgs e)
    {
        // Recupera gli elementi selezionati
        var selezionati = ListaAttrezzi.SelectedItems.Cast<AttrezziPalestre>().ToList();

        // Salva nella proprietà pubblica per poter essere letta dal chiamante
        SelectedItemsResult = selezionati;

        // Chiude il popup
        await CloseAsync();
    }
}