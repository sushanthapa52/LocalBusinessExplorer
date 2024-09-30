using Firebase.Auth;
using LocalBusinessExplorer.ViewModel;

namespace LocalBusinessExplorer.Views;

public partial class HomePage : ContentPage
{
    private readonly HomePageViewModel _viewModel;

    public HomePage(HomePageViewModel viewModel )
    {
        InitializeComponent();
        _viewModel = viewModel;
    }
    private async void OnCategoryButtonClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var categoryType = button.CommandParameter.ToString();

        await Shell.Current.GoToAsync($"///{nameof(BusinessListingsPage)}?categoryType={categoryType}");





    }
}