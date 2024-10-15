using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Windows.Input;

namespace LocalBusinessExplorer.Views;

public partial class BusinessDetails : ContentPage
{

    public BusinessDetails(object selectedBusiness, double latitude, double longitude)
    {
        InitializeComponent();
        BindingContext = selectedBusiness;
        Location businessLocation = new Location
        {
            Latitude = latitude,
            Longitude = longitude
        };
        LoadMap(businessLocation);

    }
    private void OnBackButtonClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
    public async void LoadMap(Location businessLocation)
    {
        // Get user's current location
        var currentLocation = await GetCurrentLocationAsync();

        if (currentLocation == null)
        {
            await DisplayAlert("Error", "Unable to get your location.", "OK");
            return;
        }

        // Center the map between user and business location
        var mapSpan = MapSpan.FromCenterAndRadius(
            new Location((currentLocation.Latitude + businessLocation.Latitude) / 2,
                         (currentLocation.Longitude + businessLocation.Longitude) / 2),
            Distance.FromMiles(5));

        MapView.MoveToRegion(mapSpan);

        // Fetch the directions data
        var directionsUrl = await GetDirectionsUrl(currentLocation, businessLocation);
        var directionsData = await FetchDirections(directionsUrl);

        // Access the polyline for the first route (most optimal route)
        var polylinePoints = directionsData.Routes[0].OverviewPolyline.Points;

        // Decode the polyline
        var routeCoordinates = DecodePolyline(polylinePoints);

        // Create a new polyline to display the route
        var polyline = new Polyline
        {
            StrokeColor = Colors.Blue,
            StrokeWidth = 5
        };

        // Add the decoded route coordinates to the polyline
        foreach (var position in routeCoordinates)
        {
            polyline.Geopath.Add(new Location(position.Latitude, position.Longitude));
        }

        // Add the polyline to the map
        MapView.MapElements.Add(polyline);
    }
    public async Task<Location> GetCurrentLocationAsync()
    {
        try
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            var location = await Geolocation.GetLocationAsync(request);

            if (location != null)
            {
                return location;
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions, e.g., location services not enabled
        }
        return null;
    }
    public async Task<string> GetDirectionsUrl(Location currentLocation, Location businessLocation)
    {
        var origin = $"{currentLocation.Latitude},{currentLocation.Longitude}";
        var destination = $"{businessLocation.Latitude},{businessLocation.Longitude}";
        var apiKey = "AIzaSyDiM3f8m3KA_eCtyp9WzTCSu71MTrAEJns";

        var directionsUrl = $"https://maps.googleapis.com/maps/api/directions/json?origin={origin}&destination={destination}&key={apiKey}";
        return directionsUrl;
    }
    public async Task<DirectionsData> FetchDirections(string directionsUrl)
    {
        using (var httpClient = new HttpClient())
        {
            try
            {
                // Make the HTTP request to the Google Directions API
                var response = await httpClient.GetStringAsync(directionsUrl);

                // Deserialize the response JSON into a DirectionsData object
                var directionsData = JsonConvert.DeserializeObject<DirectionsData>(response);

                // Check if the request was successful
                if (directionsData.Status == "OK")
                {
                    return directionsData;
                }
                else
                {
                    // Handle the error from the API
                    await Application.Current.MainPage.DisplayAlert("Error", "Unable to fetch directions: " + directionsData.Status, "OK");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions like network issues
                await Application.Current.MainPage.DisplayAlert("Error", "An error occurred while fetching directions: " + ex.Message, "OK");
            }
        }
        return null;
    }
    public class DirectionsData
    {
        [JsonProperty("routes")]
        public List<Route> Routes { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class Route
    {
        [JsonProperty("overview_polyline")]
        public OverviewPolyline OverviewPolyline { get; set; }
    }

    public class OverviewPolyline
    {
        [JsonProperty("points")]
        public string Points { get; set; }
    }
    public List<Location> DecodePolyline(string encodedPoints)
    {
        if (string.IsNullOrWhiteSpace(encodedPoints))
            return null;

        var poly = new List<Location>();
        var polylineChars = encodedPoints.ToCharArray();
        var index = 0;

        int currentLat = 0;
        int currentLng = 0;

        while (index < polylineChars.Length)
        {
            // Decode latitude
            int sum = 0;
            int shifter = 0;
            int next5Bits;
            do
            {
                next5Bits = polylineChars[index++] - 63;
                sum |= (next5Bits & 31) << shifter;
                shifter += 5;
            } while (next5Bits >= 32 && index < polylineChars.Length);

            if (index >= polylineChars.Length)
                break;

            currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

            // Decode longitude
            sum = 0;
            shifter = 0;
            do
            {
                next5Bits = polylineChars[index++] - 63;
                sum |= (next5Bits & 31) << shifter;
                shifter += 5;
            } while (next5Bits >= 32 && index < polylineChars.Length);

            if (index >= polylineChars.Length && next5Bits >= 32)
                break;

            currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

            var location = new Location
            {
                Latitude = currentLat / 1E5,
                Longitude = currentLng / 1E5
            };
            poly.Add(location);
        }

        return poly;
    }
}