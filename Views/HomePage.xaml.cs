using Firebase.Auth;
using LocalBusinessExplorer.ViewModel;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace LocalBusinessExplorer.Views;

public partial class HomePage : ContentPage
{
    private readonly HomePageViewModel _viewModel;


    public HomePage(HomePageViewModel viewModel )
    {
        InitializeComponent();
        _viewModel = viewModel;
        LoadMap();
    }
    private async void OnCategoryButtonClicked(object sender, EventArgs e)
    {
        string commandParameter = string.Empty;

        if (sender is Button button)
        {
            // This is a button click, so we get the command parameter from the button
            commandParameter = button.CommandParameter?.ToString();
        }
        else if (sender is SearchBar searchBar)
        {
            // This is the search bar, so we get the search text
            commandParameter = searchBar.Text;
        }

        await Shell.Current.GoToAsync($"///{nameof(BusinessListingsPage)}?categoryType={commandParameter}");

    }
    private async void LoadMap()
    {
        try
        {
            // Get the user's current location
            var location = await Geolocation.GetLastKnownLocationAsync();
            

            if (location != null)
            {
                // Move the map to center on the user's location
                var mapSpan = MapSpan.FromCenterAndRadius(new Location(location.Latitude, location.Longitude), Distance.FromKilometers(1));

                MyMap.MoveToRegion(mapSpan);


                // Add the pin to the map
            }
            else
            {
                // Handle case where location is unavailable
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions, such as location services not available
        }
    }
}