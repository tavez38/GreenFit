using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace GreenFit // Verifica che sia il tuo namespace corretto
{
    [Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)]
    [IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataScheme = "com.googleusercontent.apps.23526714735-0m5vfobvk91suses2cq2lf1k3pvqfl1a")] // Inserisci il tuo ID qui
    public class WebAuthenticationCallbackActivity : Activity // Usiamo la classe base di Android
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Questa è la riga magica per MAUI:
            // Estrae i dati dall'Intent (il link di ritorno di Google) 
            // e li "inietta" nel WebAuthenticator che sta aspettando nella LoginPage.
            Microsoft.Maui.Authentication.WebAuthenticator.Default.OnResume(Intent);

            // Chiude questa attività fantasma e torna all'app principale
            Finish();
        }
    }
}