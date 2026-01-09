namespace SportPulse.Views
{
    public partial class GameDetailPage : ContentPage
    {
        public GameDetailPage()
        {
            InitializeComponent();
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}