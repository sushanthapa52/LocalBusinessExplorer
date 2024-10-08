using LocalBusinessExplorer.ViewModel;

namespace LocalBusinessExplorer.Views;


public partial class BusinessListingsPage : ContentPage
{
	private readonly BusinessListingsViewModel _viewModel;

    public BusinessListingsPage(BusinessListingsViewModel businessListingsViewModel)
	{
		InitializeComponent();
        _viewModel = businessListingsViewModel;
        BindingContext = _viewModel;

    }
    private async void OnTitleTapped(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync($"///{nameof(HomePage)}");
    }
}