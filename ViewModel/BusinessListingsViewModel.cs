using LocalBusinessExplorer.Services;
using LocalBusinessExplorer.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls.Shapes;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;

using System.Windows.Input;

namespace LocalBusinessExplorer.ViewModel
{
    public class BusinessListingsViewModel : INotifyPropertyChanged, IQueryAttributable
    {

        public ObservableCollection<Place> Places { get; set; } = new ObservableCollection<Place>();
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;
        private readonly GooglePlaceAPI _googleService;

        public ICommand BackCommand { get; }


        public BusinessListingsViewModel(GooglePlaceAPI googleService)
        {
            _googleService = googleService;
            _apiKey = _googleService.GetGoogleApiKey();

            BackCommand = new Command(OnBackButtonClicked);

        }
        private async void OnBackButtonClicked()
        {
            await Shell.Current.GoToAsync("//HomePage");
        }

        private string _category;
        public string Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand LoadBusinessesCommand { get; }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("categoryType"))
            {
                string categoryType = query["categoryType"] as string;

                // Load the businesses based on the category type
                LoadBusinessesAsync(categoryType);
            }
        }

        public async Task LoadBusinessesAsync(string category_type)
        {
            IsBusy = true;  // Start loading (show ActivityIndicator)
            try
            {
                //var xyz = GetCurrentLocation();
                var location = await Geolocation.GetLocationAsync();
                if (location != null)
                {
                    // api key section
                    var apiKey = _apiKey;
                    var url = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={location.Latitude},{location.Longitude}&radius=5000&keyword={category_type}&key={apiKey}";

                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.GetStringAsync(url);
                        var googlePlacesResponse = JsonConvert.DeserializeObject<GooglePlacesResponse>(response);

                        // Clear the list and add new places
                        Places.Clear();
                        foreach (var place in googlePlacesResponse.Results)
                        {
                            Places.Add(place);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                // Handle any exceptions (network issues, etc.)
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class Place
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("vicinity")]
        public string Vicinity { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }


        // Geometry object to hold location information
        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }


        [JsonProperty("rating")]
        public double Rating { get; set; } // New property for business rating

        [JsonProperty("user_ratings_total")]
        public int UserRatingsTotal { get; set; } // New property for total number of ratings


        [JsonProperty("opening_hours")]
        public OpeningHours OpeningHours { get; set; }  // Add this
    }
    public class OpeningHours
    {
        [JsonProperty("open_now")]
        public bool OpenNow { get; set; }
    }

    public class GooglePlacesResponse
    {
        [JsonProperty("results")]
        public List<Place> Results { get; set; }
    }
    public class Geometry
    {
        [JsonProperty("location")]
        public Location Location { get; set; }
    }

    public class Location
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lng")]
        public double Longitude { get; set; }
    }
}
