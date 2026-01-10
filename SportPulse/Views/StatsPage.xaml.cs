using System.Collections.Generic;

namespace SportPulse.Views;

public partial class StatsPage : ContentPage
{
    private string _currentSport = "Bundesliga";
    
    public StatsPage()
    {
        InitializeComponent();
        LoadBundesligaTable();
    }

    private void OnBundesligaClicked(object sender, EventArgs e)
    {
        _currentSport = "Bundesliga";
        UpdateButtonStyles();
        LoadBundesligaTable();
    }

    private void OnNbaClicked(object sender, EventArgs e)
    {
        _currentSport = "NBA";
        UpdateButtonStyles();
        LoadNbaTable();
    }

    private void OnTopLigaClicked(object sender, EventArgs e)
    {
        _currentSport = "TopLiga";
        UpdateButtonStyles();
        LoadTopLigaTable();
    }

    private void OnF1Clicked(object sender, EventArgs e)
    {
        _currentSport = "F1";
        UpdateButtonStyles();
        LoadF1Table();
    }

    private void UpdateButtonStyles()
    {
        BundesligaBtn.BackgroundColor = _currentSport == "Bundesliga" ? Color.FromArgb("#880A21") : Color.FromArgb("#3B3B3B");
        NbaBtn.BackgroundColor = _currentSport == "NBA" ? Color.FromArgb("#880A21") : Color.FromArgb("#3B3B3B");
        TopLigaBtn.BackgroundColor = _currentSport == "TopLiga" ? Color.FromArgb("#880A21") : Color.FromArgb("#3B3B3B");
        F1Btn.BackgroundColor = _currentSport == "F1" ? Color.FromArgb("#880A21") : Color.FromArgb("#3B3B3B");
    }

    private void LoadBundesligaTable()
    {
        LeagueTitleLabel.Text = "⚽ Bundesliga";
        TeamHeaderLabel.Text = "Team";
        GoalsHeaderLabel.Text = "Tore";
        TableContent.Children.Clear();

        var teams = new List<TeamStats>
        {
            new TeamStats { Position = 1, Name = "Bayer Leverkusen", Matches = 11, Wins = 8, Draws = 2, Losses = 1, GoalsFor = 26, GoalsAgainst = 12 },
            new TeamStats { Position = 2, Name = "Bayern München", Matches = 11, Wins = 8, Draws = 3, Losses = 2, GoalsFor = 33, GoalsAgainst = 7 },
            new TeamStats { Position = 3, Name = "VfB Stuttgart", Matches = 11, Wins = 6, Draws = 5, Losses = 4, GoalsFor = 25, GoalsAgainst = 19 },
            new TeamStats { Position = 4, Name = "RB Leipzig", Matches = 11, Wins = 6, Draws = 3, Losses = 2, GoalsFor = 15, GoalsAgainst = 5 },
            new TeamStats { Position = 5, Name = "Borussia Dortmund", Matches = 11, Wins = 6, Draws = 1, Losses = 4, GoalsFor = 22, GoalsAgainst = 18 },
        };

        foreach (var team in teams)
        {
            TableContent.Children.Add(CreateTeamRow(team));
        }
    }

    private void LoadNbaTable()
    {
        LeagueTitleLabel.Text = "🏀 NBA";
        TeamHeaderLabel.Text = "Team";
        GoalsHeaderLabel.Text = "Pts";
        TableContent.Children.Clear();

        var teams = new List<TeamStats>
        {
            new TeamStats { Position = 1, Name = "Boston Celtics", Matches = 35, Wins = 27, Draws = 0, Losses = 8, GoalsFor = 3850, GoalsAgainst = 3600 },
            new TeamStats { Position = 2, Name = "Milwaukee Bucks", Matches = 34, Wins = 25, Draws = 0, Losses = 9, GoalsFor = 3780, GoalsAgainst = 3650 },
            new TeamStats { Position = 3, Name = "Philadelphia 76ers", Matches = 33, Wins = 24, Draws = 0, Losses = 9, GoalsFor = 3700, GoalsAgainst = 3550 },
            new TeamStats { Position = 4, Name = "Cleveland Cavaliers", Matches = 34, Wins = 23, Draws = 0, Losses = 11, GoalsFor = 3690, GoalsAgainst = 3620 },
            new TeamStats { Position = 5, Name = "Miami Heat", Matches = 33, Wins = 22, Draws = 0, Losses = 11, GoalsFor = 3600, GoalsAgainst = 3580 },
        };

        foreach (var team in teams)
        {
            TableContent.Children.Add(CreateTeamRow(team, isBasketball: true));
        }
    }

    private void LoadTopLigaTable()
    {
        LeagueTitleLabel.Text = "🏐 TopLiga";
        TeamHeaderLabel.Text = "Team";
        GoalsHeaderLabel.Text = "Tore";
        TableContent.Children.Clear();

        var teams = new List<TeamStats>
        {
            new TeamStats { Position = 1, Name = "Amriswil", Matches = 14, Wins = 12, Draws = 1, Losses = 1, GoalsFor = 42, GoalsAgainst = 15 },
            new TeamStats { Position = 2, Name = "Lausanne UC", Matches = 14, Wins = 11, Draws = 0, Losses = 3, GoalsFor = 38, GoalsAgainst = 18 },
            new TeamStats { Position = 3, Name = "Schönenwerd", Matches = 14, Wins = 10, Draws = 2, Losses = 2, GoalsFor = 36, GoalsAgainst = 20 },
            new TeamStats { Position = 4, Name = "Chênois Genève", Matches = 14, Wins = 9, Draws = 1, Losses = 4, GoalsFor = 33, GoalsAgainst = 24 },
            new TeamStats { Position = 5, Name = "Näfels", Matches = 14, Wins = 8, Draws = 2, Losses = 4, GoalsFor = 30, GoalsAgainst = 25 },
        };

        foreach (var team in teams)
        {
            TableContent.Children.Add(CreateTeamRow(team));
        }
    }

    private void LoadF1Table()
    {
        LeagueTitleLabel.Text = "🏎️ Formel 1";
        TeamHeaderLabel.Text = "Fahrer";
        GoalsHeaderLabel.Text = "Punkte";
        
        TableContent.Children.Clear();

        var drivers = new List<TeamStats>
        {
            new TeamStats { Position = 1, Name = "Max Verstappen", Matches = 22, Wins = 10, Draws = 0, Losses = 12, GoalsFor = 395, GoalsAgainst = 5 },
            new TeamStats { Position = 2, Name = "Lewis Hamilton", Matches = 22, Wins = 8, Draws = 0, Losses = 14, GoalsFor = 387, GoalsAgainst = 5 },
            new TeamStats { Position = 3, Name = "Valtteri Bottas", Matches = 22, Wins = 1, Draws = 0, Losses = 21, GoalsFor = 226, GoalsAgainst = 0 },
            new TeamStats { Position = 4, Name = "Sergio Pérez", Matches = 22, Wins = 1, Draws = 0, Losses = 21, GoalsFor = 190, GoalsAgainst = 0 },
            new TeamStats { Position = 5, Name = "Carlos Sainz", Matches = 22, Wins = 0, Draws = 0, Losses = 22, GoalsFor = 164, GoalsAgainst = 5 },
            new TeamStats { Position = 6, Name = "Lando Norris", Matches = 22, Wins = 0, Draws = 0, Losses = 22, GoalsFor = 160, GoalsAgainst = 0 },
            new TeamStats { Position = 7, Name = "Charles Leclerc", Matches = 22, Wins = 0, Draws = 0, Losses = 22, GoalsFor = 159, GoalsAgainst = 0 },
            new TeamStats { Position = 8, Name = "Daniel Ricciardo", Matches = 22, Wins = 1, Draws = 0, Losses = 21, GoalsFor = 115, GoalsAgainst = 0 },
        };

        foreach (var driver in drivers)
        {
            TableContent.Children.Add(CreateTeamRow(driver, isF1: true));
        }
    }

    private Frame CreateTeamRow(TeamStats team, bool isBasketball = false, bool isF1 = false)
    {
        var frame = new Frame
        {
            BackgroundColor = Color.FromArgb("#3B3B3B"),
            CornerRadius = 6,
            Padding = 8,
            HasShadow = false,
            Margin = new Thickness(0, 0, 0, 1)
        };

        var grid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection
            {
                new ColumnDefinition { Width = new GridLength(40) },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = new GridLength(35) },
                new ColumnDefinition { Width = new GridLength(35) },
                new ColumnDefinition { Width = new GridLength(35) },
                new ColumnDefinition { Width = new GridLength(40) },
                new ColumnDefinition { Width = new GridLength(40) }
            },
            ColumnSpacing = 4
        };

        grid.Add(new Label { Text = team.Position.ToString(), TextColor = Colors.White, FontSize = 14, VerticalOptions = LayoutOptions.Center }, 0);
        grid.Add(new Label { Text = team.Name, TextColor = Colors.White, FontSize = 14, VerticalOptions = LayoutOptions.Center }, 1);
        grid.Add(new Label { Text = team.Matches.ToString(), TextColor = Colors.White, FontSize = 14, HorizontalTextAlignment = TextAlignment.Center, VerticalOptions = LayoutOptions.Center }, 2);
        grid.Add(new Label { Text = team.Wins.ToString(), TextColor = Colors.White, FontSize = 14, HorizontalTextAlignment = TextAlignment.Center, VerticalOptions = LayoutOptions.Center }, 3);
        grid.Add(new Label { Text = team.Draws.ToString(), TextColor = Colors.White, FontSize = 14, HorizontalTextAlignment = TextAlignment.Center, VerticalOptions = LayoutOptions.Center }, 4);
        grid.Add(new Label { Text = team.Losses.ToString(), TextColor = Colors.White, FontSize = 14, HorizontalTextAlignment = TextAlignment.Center, VerticalOptions = LayoutOptions.Center }, 5);
        
        string goalsText;
        if (isF1)
        {
            goalsText = $"{team.GoalsFor} Pkt";
        }
        else if (isBasketball)
        {
            goalsText = $"{team.GoalsFor}";
        }
        else
        {
            goalsText = $"{team.GoalsFor}:{team.GoalsAgainst}";
        }
        
        grid.Add(new Label { Text = goalsText, TextColor = Colors.White, FontSize = 14, HorizontalTextAlignment = TextAlignment.Center, VerticalOptions = LayoutOptions.Center }, 6);

        frame.Content = grid;
        return frame;
    }

    private class TeamStats
    {
        public int Position { get; set; }
        public string Name { get; set; }
        public int Matches { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
    }
}