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
        string commandParameter = string.Empty;

        if (sender is Button button)
        {
            // This is a button click, so we get the command parameter from the button
            commandParameter = button.CommandParameter?.ToString();
        }
        else if (sender is SearchBar searchBar)
        {
            // This is the search bar, so we get the search text
            commandParameter = searchBar.Text;
        }

        await Shell.Current.GoToAsync($"///{nameof(BusinessListingsPage)}?categoryType={commandParameter}");





    }
}