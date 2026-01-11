using System.Linq;

namespace SportPulse.Views;

public partial class FavoritesPage : ContentPage
{
    public FavoritesPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadFavorites();
    }

    private void LoadFavorites()
    {
        // Prüfe ob Benutzer angemeldet ist
        var userName = Preferences.Get("user_name", string.Empty);
        
        var isLoggedIn = !string.IsNullOrEmpty(userName);

        if (!isLoggedIn)
        {
            // Zeige Login-Aufforderung
            NotLoggedInView.IsVisible = true;
            LoggedInView.IsVisible = false;
        }
        else
        {
            // Zeige Favoriten
            NotLoggedInView.IsVisible = false;
            LoggedInView.IsVisible = true;
            
            // Lade nur Favoriten-Sportarten (keine separate Lieblingssportart mehr)
            var favoriteSportsJson = Preferences.Get("favorite_sports", string.Empty);
            var favoriteSports = new HashSet<string>();
            
            if (!string.IsNullOrEmpty(favoriteSportsJson))
            {
                var sports = favoriteSportsJson.Split(',');
                foreach (var sport in sports)
                {
                    if (!string.IsNullOrEmpty(sport))
                        favoriteSports.Add(sport);
                }
            }
            
            // Lade "Meine Favoriten" - markierte Sportarten
            LoadMyFavorites();
            
            // Lade "Personalisierte Ergebnisse" - basierend auf Favoriten-Sportarten
            LoadPersonalizedResults(favoriteSports.ToList());
            
            // Zeige Empty State wenn keine Daten
            EmptyFavoritesView.IsVisible = FavoritesContent.Children.Count == 0 && PersonalizedContent.Children.Count == 0;
        }
    }

    private void LoadMyFavorites()
    {
        FavoritesContent.Children.Clear();
        
        // Lade markierte Sportarten aus Favoriten
        var favoriteSportsJson = Preferences.Get("favorite_sports", string.Empty);
        
        if (!string.IsNullOrEmpty(favoriteSportsJson))
        {
            var favoriteSports = favoriteSportsJson.Split(',');
            foreach (var sport in favoriteSports)
            {
                AddFavoriteSportCard(sport);
            }
        }
    }

    private void LoadPersonalizedResults(List<string> favoriteSports)
    {
        PersonalizedContent.Children.Clear();
        
        if (favoriteSports.Count == 0)
        {
            return;
        }
        
        foreach (var sport in favoriteSports)
        {
            LoadGamesForSport(sport);
        }
    }

    private void LoadGamesForSport(string sport)
    {
        var (emoji, _) = GetSportInfo(sport);
        
        switch (sport)
        {
            case "Fussball":
                AddGameCard(emoji, "Bayern München", "vs RB Leipzig", "33", "Heute, 15:30");
                AddGameCard(emoji, "Bayern München", "vs Union Berlin", "2:0", "17.11.2025");
                break;
                
            case "Basketball":
                AddGameCard(emoji, "Lakers", "vs Celtics", "VOR/12", "Gestern, 03:00");
                AddGameCard(emoji, "Lakers", "vs Nets", "104:98", "16.11.2025");
                break;
                
            case "Tennis":
                AddGameCard(emoji, "Nadal, R.", "vs Djokovic, N.", "", "Heute, 14:00");
                AddGameCard(emoji, "Alcaraz, C.", "vs Medvedev, D.", "", "Morgen, 16:00");
                break;
                
            case "Formel 1":
            case "F1":
                AddGameCard(emoji, "Bahrain GP", "", "", "Morgen, 17:00");
                AddGameCard(emoji, "Saudi-Arabien GP", "", "", "Nächste Woche");
                break;
                
            case "WEC":
                AddGameCard(emoji, "6h of Spa", "", "", "Nächste Woche");
                AddGameCard(emoji, "24h Le Mans", "", "", "Juni 2026");
                break;
                
            case "Volleyball":
                AddGameCard(emoji, "Amriswil", "vs Lausanne UC", "3:1", "Heute, 18:00");
                AddGameCard(emoji, "Schönenwerd", "vs Chênois", "", "Morgen, 19:30");
                break;
        }
    }

    private void AddGameCard(string emoji, string team1, string team2, string score, string time)
    {
        var frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#1F1F1F"),
            CornerRadius = 8,
            Padding = 12,
            HasShadow = false,
            Margin = new Thickness(0, 0, 0, 0)
        };

        var mainStack = new VerticalStackLayout { Spacing = 4 };

        // Team 1 / Event Name
        var team1Label = new Label
        {
            Text = $"{emoji} {team1}",
            TextColor = Colors.White,
            FontSize = 15,
            FontAttributes = FontAttributes.Bold
        };
        mainStack.Add(team1Label);

        // Team 2 / vs (if exists)
        if (!string.IsNullOrEmpty(team2))
        {
            var team2Label = new Label
            {
                Text = team2,
                TextColor = Color.FromArgb("#CCCCCC"),
                FontSize = 14
            };
            mainStack.Add(team2Label);
        }

        // Score (if exists)
        if (!string.IsNullOrEmpty(score))
        {
            var scoreLabel = new Label
            {
                Text = score,
                TextColor = Color.FromArgb("#ff3b30"),
                FontSize = 13,
                FontAttributes = FontAttributes.Bold
            };
            mainStack.Add(scoreLabel);
        }

        // Time/Date
        var timeLabel = new Label
        {
            Text = time,
            TextColor = Color.FromArgb("#999999"),
            FontSize = 12
        };
        mainStack.Add(timeLabel);

        frame.Content = mainStack;
        PersonalizedContent.Children.Add(frame);
    }

    private void AddFavoriteSportCard(string sport)
    {
        var (emoji, sportName) = GetSportInfo(sport);
        
        var frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#1F1F1F"),
            CornerRadius = 8,
            Padding = 12,
            HasShadow = false,
            Margin = new Thickness(0, 0, 0, 0)
        };

        var grid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto }
            },
            ColumnSpacing = 12
        };

        // Pin Icon
        var pinLabel = new Label
        {
            Text = "📌",
            FontSize = 18,
            VerticalOptions = LayoutOptions.Center
        };
        grid.Add(pinLabel, 0);

        // Sport Name
        var sportLabel = new Label
        {
            Text = $"{emoji} {sportName}",
            TextColor = Colors.White,
            FontSize = 15,
            FontAttributes = FontAttributes.Bold,
            VerticalOptions = LayoutOptions.Center
        };
        grid.Add(sportLabel, 1);

        // Remove Button (X)
        var removeButton = new Label
        {
            Text = "✕",
            TextColor = Color.FromArgb("#ff3b30"),
            FontSize = 18,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.End
        };
        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += (s, e) => OnRemoveFavoriteClicked(sport);
        removeButton.GestureRecognizers.Add(tapGesture);
        grid.Add(removeButton, 2);

        frame.Content = grid;
        FavoritesContent.Children.Add(frame);
    }

    private void AddPersonalizedSportCard(string sport)
    {
        var (emoji, sportName) = GetSportInfo(sport);
        
        var frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#1F1F1F"),
            CornerRadius = 8,
            Padding = 12,
            HasShadow = false,
            Margin = new Thickness(0, 0, 0, 0)
        };

        var mainStack = new VerticalStackLayout { Spacing = 4 };

        // Sport Name
        var sportLabel = new Label
        {
            Text = $"{emoji} {sportName}",
            TextColor = Colors.White,
            FontSize = 15,
            FontAttributes = FontAttributes.Bold
        };
        mainStack.Add(sportLabel);

        // Info text based on sport
        var infoText = sport switch
        {
            "Fussball" => "Aktuelle Bundesliga Spiele",
            "Basketball" => "NBA Live Spiele",
            "Tennis" => "ATP Turniere",
            "F1" => "Formel 1 Rennen",
            "WEC" => "WEC Langstreckenrennen",
            "Volleyball" => "TopLiga Spiele",
            _ => "Aktuelle Spiele"
        };

        var infoLabel = new Label
        {
            Text = infoText,
            TextColor = Color.FromArgb("#999999"),
            FontSize = 12
        };
        mainStack.Add(infoLabel);

        frame.Content = mainStack;
        PersonalizedContent.Children.Add(frame);
    }

    private (string emoji, string name) GetSportInfo(string sport)
    {
        return sport switch
        {
            "Fussball" => ("⚽", "Fussball"),
            "Basketball" => ("🏀", "Basketball"),
            "Tennis" => ("🎾", "Tennis"),
            "F1" => ("🏎️", "Formel 1"),
            "WEC" => ("🏁", "WEC"),
            "Volleyball" => ("🏐", "Volleyball"),
            _ => ("", sport)
        };
    }

    private void OnRemoveFavoriteClicked(string sport)
    {
        // Entferne Sportart aus Favoriten
        var favoriteSportsJson = Preferences.Get("favorite_sports", string.Empty);
        
        if (!string.IsNullOrEmpty(favoriteSportsJson))
        {
            var favoriteSports = favoriteSportsJson.Split(',')
                .Where(s => !string.IsNullOrEmpty(s) && s != sport)
                .ToList();
            
            // Speichere aktualisierte Favoriten
            var updatedJson = string.Join(",", favoriteSports);
            Preferences.Set("favorite_sports", updatedJson);
            
            // Lade Favoriten neu
            LoadFavorites();
        }
    }

    private async void OnAddFavoriteClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Hinzufügen", "Gehe zur Live-Seite und markiere Sportarten mit dem Stern als Favoriten.", "OK");
    }


    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        // Navigate zur ProfilePage
        await Shell.Current.GoToAsync("//profile");
    }
}