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
}