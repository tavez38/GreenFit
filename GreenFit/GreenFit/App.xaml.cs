

using GreenFit.Serivces;

namespace GreenFit
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
           FileManager.CreateFileIfNotExists();
           FileManager.readFileText();
           
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            //return new Window(new AppShell());
            return new Window(new Pages.ChatBotPage());
            
        }
    }
}