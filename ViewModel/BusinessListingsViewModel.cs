using LocalBusinessExplorer.Services;
using Microsoft.Extensions.Configuration;
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



        public BusinessListingsViewModel(GooglePlaceAPI googleService)
        {
            _googleService = googleService;
            _apiKey = _googleService.GetGoogleApiKey();

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
                    var url = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={location.Latitude},{location.Longitude}&radius=5000&type={category_type}&key={apiKey}";

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

        // Other fields can be added here based on what you need
    }

    public class GooglePlacesResponse
    {
        [JsonProperty("results")]
        public List<Place> Results { get; set; }
    }
}
