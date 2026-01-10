
namespace SportPulse.Views;

public partial class MainPage : ContentPage
{
    private string _currentFilter = "Alle";
    
    public MainPage()
    {
        InitializeComponent();
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

    private void UpdateFilterButtons()
    {
        AllBtn.BackgroundColor = _currentFilter == "Alle" ? Color.FromArgb("#880A21") : Color.FromArgb("#3B3B3B");
        FootballBtn.BackgroundColor = _currentFilter == "Fussball" ? Color.FromArgb("#880A21") : Color.FromArgb("#3B3B3B");
        BasketballBtn.BackgroundColor = _currentFilter == "Basketball" ? Color.FromArgb("#880A21") : Color.FromArgb("#3B3B3B");
        TennisBtn.BackgroundColor = _currentFilter == "Tennis" ? Color.FromArgb("#880A21") : Color.FromArgb("#3B3B3B");
        F1Btn.BackgroundColor = _currentFilter == "F1" ? Color.FromArgb("#880A21") : Color.FromArgb("#3B3B3B");
        WecBtn.BackgroundColor = _currentFilter == "WEC" ? Color.FromArgb("#880A21") : Color.FromArgb("#3B3B3B");
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