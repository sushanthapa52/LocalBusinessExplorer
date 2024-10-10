using LocalBusinessExplorer.Entities;
using LocalBusinessExplorer.Services;
using LocalBusinessExplorer.ViewModel;

namespace LocalBusinessExplorer.Views;


public partial class EventsPage : ContentPage
{
    private readonly EventsPagetViewModel _viewmodel;
    private readonly FirebaseDb _firebaseDb;
    private readonly EventDataService _eventDataService;
    public EventsPage(EventsPagetViewModel viewModel, FirebaseDb firebaseDb, EventDataService eventDataService)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewmodel = viewModel;
        _firebaseDb = firebaseDb;
        _eventDataService = eventDataService;
    }
    private async void OnTitleTapped(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync($"///{nameof(HomePage)}");
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

    // Event handler for the Fetch Events button click

    // Event handler for the Logout button click
    private async void OnLogoutButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"///{nameof(LoginPage)}");

    }

}