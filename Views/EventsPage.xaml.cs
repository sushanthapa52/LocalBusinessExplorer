using LocalBusinessExplorer.ViewModel;

namespace LocalBusinessExplorer.Views;


public partial class EventsPage : ContentPage
{
    private readonly EventsPagetViewModel _viewmodel;

    public EventsPage(EventsPagetViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewmodel = viewModel;
    }
    private async void OnTitleTapped(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync($"///{nameof(HomePage)}");
    }

}