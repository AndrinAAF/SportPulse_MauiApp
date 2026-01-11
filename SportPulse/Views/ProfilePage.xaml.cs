using System.Text.RegularExpressions;

namespace SportPulse.Views;

public partial class ProfilePage : ContentPage
{
    private bool _isLoggedIn;
    private bool _isNameValid;
    private bool _isEmailValid;
    private bool _isSportSelected;

    public ProfilePage()
    {
        InitializeComponent();
        LoadUserProfile();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadUserProfile();
    }

    private void LoadUserProfile()
    {
        // Prüfe ob Benutzerdaten gespeichert sind
        var name = Preferences.Get("user_name", string.Empty);
        var email = Preferences.Get("user_email", string.Empty);
        var sport = Preferences.Get("user_sport", string.Empty);
        var notifications = Preferences.Get("user_notifications", false);
        var favoriteSportsJson = Preferences.Get("favorite_sports", string.Empty);

        _isLoggedIn = !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email);

        if (_isLoggedIn)
        {
            // Zeige Profil-Ansicht
            LoginForm.IsVisible = false;
            ProfileView.IsVisible = true;

            ProfileName.Text = name;
            ProfileEmail.Text = email;
            ProfileSport.Text = sport;
            ProfileNotificationSwitch.IsToggled = notifications;
            
            // Zeige Favoriten-Sportarten
            if (!string.IsNullOrEmpty(favoriteSportsJson))
            {
                var favorites = favoriteSportsJson.Split(',');
                ProfileFavorites.Text = string.Join(", ", favorites);
            }
            else
            {
                ProfileFavorites.Text = "Keine Favoriten ausgewählt";
            }
        }
        else
        {
            // Zeige Login-Formular
            LoginForm.IsVisible = true;
            ProfileView.IsVisible = false;
        }
    }

    private void OnNameChanged(object sender, TextChangedEventArgs e)
    {
        var name = e.NewTextValue?.Trim();
        _isNameValid = !string.IsNullOrEmpty(name) && name.Length >= 2;
        NameError.IsVisible = !_isNameValid && !string.IsNullOrEmpty(name);
        UpdateSaveButtonState();
    }

    private void OnEmailChanged(object sender, TextChangedEventArgs e)
    {
        var email = e.NewTextValue?.Trim() ?? string.Empty;
        _isEmailValid = IsValidEmail(email);
        EmailError.IsVisible = !_isEmailValid && !string.IsNullOrEmpty(email);
        UpdateSaveButtonState();
    }

    private void OnSportChanged(object sender, EventArgs e)
    {
        _isSportSelected = SportPicker.SelectedIndex >= 0;
        SportError.IsVisible = false;
        UpdateSaveButtonState();
    }

    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            // Einfache Email-Validierung mit Regex
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }
        catch
        {
            return false;
        }
    }

    private void UpdateSaveButtonState()
    {
        SaveButton.IsEnabled = _isNameValid && _isEmailValid && _isSportSelected;
        SaveButton.Opacity = SaveButton.IsEnabled ? 1.0 : 0.5;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Validiere alle Felder nochmal
        var name = NameEntry.Text?.Trim() ?? string.Empty;
        var email = EmailEntry.Text?.Trim() ?? string.Empty;
        var sportIndex = SportPicker.SelectedIndex;

        // Zeige Fehler an, falls vorhanden
        NameError.IsVisible = string.IsNullOrEmpty(name);
        EmailError.IsVisible = !IsValidEmail(email);
        SportError.IsVisible = sportIndex < 0;

        if (string.IsNullOrEmpty(name) || !IsValidEmail(email) || sportIndex < 0)
        {
            await DisplayAlert("Fehler", "Bitte fülle alle Pflichtfelder korrekt aus.", "OK");
            return;
        }

        // Speichere Benutzerdaten
        var sport = SportPicker.Items[sportIndex];
        var notifications = NotificationSwitch.IsToggled;

        Preferences.Set("user_name", name);
        Preferences.Set("user_email", email);
        Preferences.Set("user_sport", sport);
        Preferences.Set("user_notifications", notifications);

        _isLoggedIn = true;

        // Zeige Erfolgs-Nachricht
        await DisplayAlert("Erfolg", "Dein Profil wurde erfolgreich gespeichert!", "OK");

        // Wechsle zur Profil-Ansicht
        LoadUserProfile();
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        var confirm = await DisplayAlert("Abmelden", "Möchtest du dich wirklich abmelden?", "Ja", "Abbrechen");

        if (confirm)
        {
            // Lösche Benutzerdaten
            Preferences.Remove("user_name");
            Preferences.Remove("user_email");
            Preferences.Remove("user_sport");
            Preferences.Remove("user_notifications");

            _isLoggedIn = false;

            // Zeige Login-Formular
            LoadUserProfile();

            // Setze Formular zurück
            NameEntry.Text = string.Empty;
            EmailEntry.Text = string.Empty;
            SportPicker.SelectedIndex = -1;
            NotificationSwitch.IsToggled = false;
        }
    }
}