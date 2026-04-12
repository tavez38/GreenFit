namespace GreenFit
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("MapPage", typeof(GreenFit.Pages.NewPage1));
        }
    }
}
