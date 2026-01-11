namespace SportPulse.Views;

public partial class InfoPage : ContentPage
{
    public InfoPage()
    {
        InitializeComponent();
    }

    private async void OnEmailTapped(object sender, EventArgs e)
    {
        try
        {
            // Öffne Email-Client mit vorausgefüllter Adresse
            var uri = new Uri("mailto:support@sportpulse.com");
            await Launcher.OpenAsync(uri);
        }
        catch
        {
            await DisplayAlert("Fehler", "E-Mail-Client konnte nicht geöffnet werden.", "OK");
        }
    }

    private async void OnWebsiteTapped(object sender, EventArgs e)
    {
        try
        {
            // Öffne Website im Browser
            var uri = new Uri("https://www.sportpulse.com");
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch
        {
            await DisplayAlert("Fehler", "Website konnte nicht geöffnet werden.", "OK");
        }
    }

    private async void OnSocialMediaTapped(object sender, EventArgs e)
    {
        try
        {
            // Zeige Auswahl-Dialog für verschiedene Social Media Plattformen
            var action = await DisplayActionSheet(
                "Folge uns auf Social Media", 
                "Abbrechen", 
                null, 
                "Twitter/X", 
                "Instagram", 
                "Facebook"
            );

            Uri? uri = null;
            switch (action)
            {
                case "Twitter/X":
                    uri = new Uri("https://twitter.com/SportPulseApp");
                    break;
                case "Instagram":
                    uri = new Uri("https://instagram.com/SportPulseApp");
                    break;
                case "Facebook":
                    uri = new Uri("https://facebook.com/SportPulseApp");
                    break;
            }

            if (uri != null)
            {
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
        }
        catch
        {
            await DisplayAlert("Fehler", "Link konnte nicht geöffnet werden.", "OK");
        }
    }
}