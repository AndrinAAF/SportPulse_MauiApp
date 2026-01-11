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
        var userSport = Preferences.Get("user_sport", string.Empty);
        
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
            
            // Setze Lieblingssportart
            FavoriteSportLabel.Text = userSport;
            
            // Lade Favoriten basierend auf Sportart
            LoadFavoritesForSport(userSport);
        }
    }

    private void LoadFavoritesForSport(string sport)
    {
        FavoritesContent.Children.Clear();

        // Erstelle Beispiel-Favoriten basierend auf der gewählten Sportart
        switch (sport)
        {
            case "Fussball":
                AddFavoriteCard("⚽", "Bayern München vs Dortmund", "2:3", "90' + 2", true);
                AddFavoriteCard("⚽", "Real Madrid vs FC Barcelona", "1:1", "45' + 2", true);
                AddFavoriteCard("⚽", "Manchester United vs Liverpool", "Heute 20:00", "", false);
                break;
                
            case "Basketball":
                AddFavoriteCard("🏀", "Lakers vs Warriors", "99:104", "Q3 8:45", true);
                AddFavoriteCard("🏀", "Celtics vs Heat", "87:82", "Q4 3:21", true);
                AddFavoriteCard("🏀", "Bucks vs Nets", "Heute 19:30", "", false);
                break;
                
            case "Tennis":
                AddFavoriteCard("🎾", "Nadal, R. vs Djokovic, N.", "3:4", "3. Satz", true);
                AddFavoriteCard("🎾", "Alcaraz vs Medvedev", "Heute 14:00", "", false);
                break;
                
            case "Formel 1":
                AddFavoriteCard("🏎️", "Bahrain GP", "Runde 42/58", "Verstappen führt", true);
                AddFavoriteCard("🏎️", "Saudi-Arabien GP", "Morgen 17:00", "", false);
                break;
                
            case "WEC":
                AddFavoriteCard("🏁", "6h of Spa", "2h 15m verbleibend", "#8 Toyota führt", true);
                AddFavoriteCard("🏁", "24h Le Mans", "Nächste Woche", "", false);
                break;
                
            case "Volleyball":
                AddFavoriteCard("🏐", "Amriswil vs Lausanne UC", "3:1", "4. Satz", true);
                AddFavoriteCard("🏐", "Schönenwerd vs Chênois", "Heute 18:00", "", false);
                break;
                
            default:
                // Zeige leere Favoriten-Ansicht
                EmptyFavoritesView.IsVisible = true;
                break;
        }

        // Wenn keine Favoriten, zeige Empty State
        if (FavoritesContent.Children.Count == 0)
        {
            EmptyFavoritesView.IsVisible = true;
        }
        else
        {
            EmptyFavoritesView.IsVisible = false;
        }
    }

    private void AddFavoriteCard(string emoji, string title, string score, string time, bool isLive)
    {
        var frame = new Frame
        {
            CornerRadius = 8,
            BackgroundColor = Color.FromArgb("#1F1F1F"),
            BorderColor = Color.FromArgb("#3A3A3A"),
            Padding = 12,
            HasShadow = false
        };

        var mainStack = new VerticalStackLayout { Spacing = 8 };

        // Header mit Sportart und Live-Badge
        var headerGrid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto }
            }
        };

        var sportLabel = new Label
        {
            Text = $"{emoji} {GetSportName(emoji)}",
            TextColor = Colors.White,
            FontSize = 14,
            FontAttributes = FontAttributes.Bold
        };
        headerGrid.Add(sportLabel, 0);

        if (isLive)
        {
            var liveBadge = new Frame
            {
                BackgroundColor = Color.FromArgb("#ff3b30"),
                Padding = new Thickness(6, 2),
                CornerRadius = 12,
                HorizontalOptions = LayoutOptions.End,
                HasShadow = false
            };
            liveBadge.Content = new Label
            {
                Text = "LIVE",
                TextColor = Colors.White,
                FontSize = 12
            };
            headerGrid.Add(liveBadge, 1);
        }

        mainStack.Add(headerGrid);

        // Match Info
        var matchStack = new StackLayout { Spacing = 6 };

        var matchLabel = new Label
        {
            Text = title,
            TextColor = Color.FromArgb("#DDDDDD"),
            FontSize = 15
        };
        matchStack.Add(matchLabel);

        if (!string.IsNullOrEmpty(score))
        {
            var scoreLabel = new Label
            {
                Text = score,
                TextColor = Colors.White,
                FontSize = 18,
                FontAttributes = FontAttributes.Bold
            };
            matchStack.Add(scoreLabel);
        }

        if (!string.IsNullOrEmpty(time))
        {
            var timeLabel = new Label
            {
                Text = time,
                TextColor = Color.FromArgb("#999999"),
                FontSize = 12
            };
            matchStack.Add(timeLabel);
        }

        mainStack.Add(matchStack);
        frame.Content = mainStack;
        
        FavoritesContent.Children.Add(frame);
    }

    private string GetSportName(string emoji)
    {
        return emoji switch
        {
            "⚽" => "Fussball",
            "🏀" => "Basketball",
            "🎾" => "Tennis",
            "🏎️" => "Formel 1",
            "🏁" => "WEC",
            "🏐" => "Volleyball",
            _ => "Sport"
        };
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        // Navigate zur ProfilePage
        await Shell.Current.GoToAsync("//profile");
    }
}