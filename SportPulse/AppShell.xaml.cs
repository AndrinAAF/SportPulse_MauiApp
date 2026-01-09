using SportPulse.Views;

namespace SportPulse
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            // Register detail page route
            Routing.RegisterRoute("gamedetail", typeof(GameDetailPage));
        }
    }
}