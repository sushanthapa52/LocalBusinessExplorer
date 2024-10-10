using Firebase.Auth;
using LocalBusinessExplorer.ViewModel;

namespace LocalBusinessExplorer.Views;

public partial class LoginPage : ContentPage
{
    private LoginViewModel _viewModel;

    public LoginPage(LoginViewModel viewModel )
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    public async void OnLoginClicked(object sender, EventArgs e)
    {
        var email = emailEntry.Text;
        var password = passwordEntry.Text;

        //if (string.IsNullOrWhiteSpace(email))
        //{
        //    await DisplayAlert("Error", "Please enter a valid email address.", "OK");
        //    return;

        //}

        //if (string.IsNullOrWhiteSpace(password)) // Adjust length as necessary
        //{
        //    await DisplayAlert("Error", "Password shoud not be empty.", "OK");
        //    return;
        //}
        var token = await _viewModel.Login(email, password);

        //if (token is null)
        //{
        //    await DisplayAlert("Error", "Login failed. Please sign up first.", "OK");
        //    return;

        //}
       
        await Shell.Current.GoToAsync($"///{nameof(HomePage)}?token={token}");


    }

    private async void OnSignUpClicked(object sender, EventArgs e)
    {
        // Navigate to Sign Up Page
        await Shell.Current.GoToAsync($"///{nameof(SignUpPage)}");
    }

}