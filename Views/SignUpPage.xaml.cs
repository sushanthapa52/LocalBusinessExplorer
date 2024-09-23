using LocalBusinessExplorer.ViewModel;

namespace LocalBusinessExplorer.Views;

public partial class SignUpPage : ContentPage
{
    private readonly SignUpViewModel _viewModel;
    public SignUpPage(SignUpViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private async void OnSignUpClicked(object sender, EventArgs e)
    {
        string email = emailEntry.Text;
        string password = passwordEntry.Text;
        string confirmPassword = confirmPasswordEntry.Text;

        // Validate inputs (basic example)
        if (string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password) ||
            password != confirmPassword)
        {
            await DisplayAlert("Error", "Please enter valid details.", "OK");
            return;
        }

        _viewModel.SignUp(email, password);

    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        // Navigate to the login page
        await Shell.Current.GoToAsync($"///{nameof(LoginPage)}");
    }
}