using LocalBusinessExplorer.Entities;
using LocalBusinessExplorer.Services;
using LocalBusinessExplorer.ViewModel;

namespace LocalBusinessExplorer.Views;


public partial class BusinessListingsPage : ContentPage
{
	private readonly BusinessListingsViewModel _viewModel;

    private readonly FirebaseDb _firebaseDb;
    private readonly EventDataService _eventDataService;

    public BusinessListingsPage(BusinessListingsViewModel businessListingsViewModel, FirebaseDb firebaseDb, EventDataService eventDataService)
	{
		InitializeComponent();
        _viewModel = businessListingsViewModel;
        BindingContext = _viewModel;
        _firebaseDb = firebaseDb;
        _eventDataService = eventDataService;
        BusinessListView.ItemTapped += OnItemTapped;


    }
    private async void OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item == null)
            return;

        // Navigate to the BusinessDetailsPage and pass the selected business
        var selectedBusiness = (Place)e.Item;
        double latitude = selectedBusiness.Geometry.Location.Latitude;
        double longitude = selectedBusiness.Geometry.Location.Longitude;


        await Navigation.PushAsync(new BusinessDetails(selectedBusiness,latitude, longitude));

        // Deselect the item
        ((ListView)sender).SelectedItem = null;
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