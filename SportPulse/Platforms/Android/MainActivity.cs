using Android.App;
using Android.Content.PM;
using Android.OS;

namespace SportPulse;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Setze die Statusbar-Farbe auf Rot, damit oben nicht mehr das alte Blau sichtbar ist
        try
        {
            var win = Window;
            if (win != null)
            {
                win.SetStatusBarColor(Android.Graphics.Color.ParseColor("#8B1C23"));
            }
        }
        catch
        {
            // Falls die API oder Parse fehlschlägt, nicht fatal - nur Best-Effort
        }
    }
}