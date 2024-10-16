
using LocalBusinessExplorer.Entities;
using LocalBusinessExplorer.Services;
using LocalBusinessExplorer.ViewModel;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;


namespace LocalBusinessExplorer.Views;

public partial class HomePage : ContentPage
{
    private readonly HomePageViewModel _viewModel;

    private readonly FirebaseDb _firebaseDb;
    private readonly EventDataService _eventDataService;

    public HomePage(HomePageViewModel viewModel, FirebaseDb firebaseDb, EventDataService eventDataService)
    {
        InitializeComponent();
        _viewModel = viewModel;
        LoadMap();
        _firebaseDb = firebaseDb;
        _eventDataService = eventDataService;

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
            var location = await Geolocation.GetLocationAsync();
            

            if (location != null)
            {
                // Move the map to center on the user's location
                var mapSpan = MapSpan.FromCenterAndRadius(new Microsoft.Maui.Devices.Sensors.Location(location.Latitude, location.Longitude), Distance.FromKilometers(3));

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

    public async void OnFetchEventsAsyncClicked(object sender, EventArgs e)
    {

        var events = await _firebaseDb.EventResults();
        _eventDataService.EventList = events;


        await Shell.Current.GoToAsync($"///{nameof(EventsPage)}");


    }
    // Event handler for the Home button click
    private async void OnHomeButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"///{nameof(HomePage)}");


    }



    private async void OnLogoutButtonClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Logout", "Do you really want to log out?", "Yes", "No");
        if (answer)
        {
            await Shell.Current.GoToAsync($"///{nameof(LoginPage)}");
        }

    }

}