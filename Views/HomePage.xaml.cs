using Firebase.Auth;
using LocalBusinessExplorer.ViewModel;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Newtonsoft.Json.Linq;
using SerpApi;
using System.Collections;

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

    public async void OnFetchEventsAsyncClicked(object sender, EventArgs e)
    {
       
            string apiKey = "8f5bf0a8e0738550c16a52280e882e72760346de7e2525dbf8d5e14be9824fcb";
            var location = "Sudbury";

            // Example: Use "theater" as event type, and customize query parameters
            Hashtable ht = new Hashtable();
            ht.Add("engine", "google_events");
            ht.Add("q", $"Events in  {location}");
            ht.Add("hl", "en");
            ht.Add("gl", "us");
        try
        {
            GoogleSearch search = new GoogleSearch(ht, apiKey);
            JObject data = search.GetJson();
            var eventsResults = data["events_results"];

            // Process and display the results
            if (eventsResults != null)
            {
                foreach (var result in eventsResults)
                {
                    string eventTitle = result["title"]?.ToString();
                    string eventVenue = result["venue"]["name"]?.ToString();
                    string eventDate = result["date"]["start_date"]?.ToString();
                }
            }
            else
            {
                Console.WriteLine("No events found.");
            }
        }
        catch(Exception ex)
        {

        }

    }
    async Task<string> GetCityNameAsync(double latitude, double longitude)
    {
        try
        {
            var placemarks = await Geocoding.GetPlacemarksAsync(latitude, longitude);
            var placemark = placemarks?.FirstOrDefault();

            if (placemark != null)
            {
                // Get the city name from the placemark
                // return placemark.Locality; // This is typically the city name
                return "Toronto";
            }

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during reverse geocoding: {ex.Message}");
            return null;
        }
    }

}