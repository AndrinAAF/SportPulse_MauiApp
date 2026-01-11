namespace SportPulse.Views;

public partial class MainPage : ContentPage
{
    private string _currentFilter = "Alle";
    private HashSet<string> _favoriteSports = new HashSet<string>();
    
    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadFavorites();
        UpdateStarIcons();
    }

    private void LoadFavorites()
    {
        // Lade gespeicherte Favoriten aus Preferences
        var favoritesJson = Preferences.Get("favorite_sports", string.Empty);
        
        if (!string.IsNullOrEmpty(favoritesJson))
        {
            try
            {
                var favorites = favoritesJson.Split(',');
                _favoriteSports = new HashSet<string>();
                foreach (var fav in favorites)
                {
                    if (!string.IsNullOrEmpty(fav))
                        _favoriteSports.Add(fav);
                }
            }
            catch
            {
                _favoriteSports = new HashSet<string>();
            }
        }
    }

    private void SaveFavorites()
    {
        // Speichere Favoriten in Preferences
        var favoritesJson = string.Join(",", _favoriteSports);
        Preferences.Set("favorite_sports", favoritesJson);
    }

    private void UpdateStarIcons()
    {
        // Update star icons basierend auf Favoriten-Status
        FootballStar.Text = _favoriteSports.Contains("Fussball") ? "★" : "☆";
        BasketballStar.Text = _favoriteSports.Contains("Basketball") ? "★" : "☆";
        TennisStar.Text = _favoriteSports.Contains("Tennis") ? "★" : "☆";
        F1Star.Text = _favoriteSports.Contains("F1") ? "★" : "☆";
        WecStar.Text = _favoriteSports.Contains("WEC") ? "★" : "☆";
        
        // Ändere Farbe für gefüllte Sterne
        FootballStar.TextColor = _favoriteSports.Contains("Fussball") ? Color.FromArgb("#FFD700") : Colors.White;
        BasketballStar.TextColor = _favoriteSports.Contains("Basketball") ? Color.FromArgb("#FFD700") : Colors.White;
        TennisStar.TextColor = _favoriteSports.Contains("Tennis") ? Color.FromArgb("#FFD700") : Colors.White;
        F1Star.TextColor = _favoriteSports.Contains("F1") ? Color.FromArgb("#FFD700") : Colors.White;
        WecStar.TextColor = _favoriteSports.Contains("WEC") ? Color.FromArgb("#FFD700") : Colors.White;
    }

    private async void OnStarTapped(object sender, EventArgs e)
    {
        // Hole das CommandParameter aus dem TapGestureRecognizer
        string? sport = null;
        
        if (sender is TapGestureRecognizer tapGesture)
        {
            sport = tapGesture.CommandParameter as string;
        }
        else if (sender is Label label && label.GestureRecognizers.Count > 0)
        {
            var gesture = label.GestureRecognizers[0] as TapGestureRecognizer;
            sport = gesture?.CommandParameter as string;
        }
        
        if (string.IsNullOrEmpty(sport))
            return;
        
        // Prüfe ob Benutzer angemeldet ist
        var userName = Preferences.Get("user_name", string.Empty);
        
        if (string.IsNullOrEmpty(userName))
        {
            // Zeige Login-Aufforderung
            var result = await DisplayAlert(
                "Anmeldung erforderlich", 
                "Um Favoriten zu speichern, musst du dich zuerst anmelden. Möchtest du jetzt zum Profil gehen?", 
                "Ja", 
                "Abbrechen"
            );
            
            if (result)
            {
                // Navigate zur ProfilePage
                await Shell.Current.GoToAsync("//profile");
            }
            
            return;
        }
        
        // Toggle Favorit
        if (_favoriteSports.Contains(sport))
        {
            _favoriteSports.Remove(sport);
            await DisplayAlert("Favorit entfernt", $"{sport} wurde aus deinen Favoriten entfernt.", "OK");
        }
        else
        {
            _favoriteSports.Add(sport);
            await DisplayAlert("Favorit hinzugefügt", $"{sport} wurde zu deinen Favoriten hinzugefügt!", "OK");
        }
        
        SaveFavorites();
        UpdateStarIcons();
    }

    private void OnFilterClicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            _currentFilter = button.Text;
            UpdateFilterButtons();
            ApplyFilter();
        }
    }

    private void OnFilterLabelClicked(object sender, EventArgs e)
    {
        // Hole das CommandParameter aus dem TapGestureRecognizer
        string? sport = null;
        
        if (sender is Label label && label.GestureRecognizers.Count > 0)
        {
            var gesture = label.GestureRecognizers[0] as TapGestureRecognizer;
            sport = gesture?.CommandParameter as string;
        }
        
        if (!string.IsNullOrEmpty(sport))
        {
            _currentFilter = sport;
            UpdateFilterButtons();
            ApplyFilter();
        }
    }

    private void UpdateFilterButtons()
    {
        AllBtn.BackgroundColor = _currentFilter == "Alle" ? Color.FromArgb("#880A21") : Color.FromArgb("#3B3B3B");
        
        // Update Frame-based button colors by finding parent Frame of labels
        UpdateFrameColor("Fussball", _currentFilter == "Fussball");
        UpdateFrameColor("Basketball", _currentFilter == "Basketball");
        UpdateFrameColor("Tennis", _currentFilter == "Tennis");
        UpdateFrameColor("F1", _currentFilter == "F1");
        UpdateFrameColor("WEC", _currentFilter == "WEC");
    }

    private void UpdateFrameColor(string sport, bool isSelected)
    {
        var color = isSelected ? Color.FromArgb("#880A21") : Color.FromArgb("#3B3B3B");
        
        // Diese Methode wird verwendet, um die Frame-Farben zu aktualisieren
        // Da wir keinen direkten Zugriff auf die Frames haben, verwenden wir die Parent-Hierarchie
        Label? starLabel = sport switch
        {
            "Fussball" => FootballStar,
            "Basketball" => BasketballStar,
            "Tennis" => TennisStar,
            "F1" => F1Star,
            "WEC" => WecStar,
            _ => null
        };

        if (starLabel?.Parent?.Parent is Frame frame)
        {
            frame.BackgroundColor = color;
        }
    }

    private void ApplyFilter()
    {
        // Alle Karten durchgehen und je nach Filter anzeigen/verbergen
        FootballCard1.IsVisible = _currentFilter == "Alle" || _currentFilter == "Fussball";
        FootballCard2.IsVisible = _currentFilter == "Alle" || _currentFilter == "Fussball";
        
        BasketballCard1.IsVisible = _currentFilter == "Alle" || _currentFilter == "Basketball";
        BasketballCard2.IsVisible = _currentFilter == "Alle" || _currentFilter == "Basketball";
        
        TennisCard1.IsVisible = _currentFilter == "Alle" || _currentFilter == "Tennis";
        
        F1Card1.IsVisible = _currentFilter == "Alle" || _currentFilter == "F1";
        
        WecCard1.IsVisible = _currentFilter == "Alle" || _currentFilter == "WEC";
    }
}