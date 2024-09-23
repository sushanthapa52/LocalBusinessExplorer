using LocalBusinessExplorer.ViewModel;

namespace LocalBusinessExplorer.Views;

public partial class HomePage : ContentPage
{
    private HomePageViewModel _viewModel;

    public HomePage(HomePageViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}