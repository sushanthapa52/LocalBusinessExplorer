using LocalBusinessExplorer.ViewModel;

namespace LocalBusinessExplorer.Views;

public partial class LoginPage : ContentPage
{
    private LoginViewModel _viewModel;

    public LoginPage(LoginViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    public async void OnLoginClicked(object sender, EventArgs e)
    {
        var email = emailEntry.Text;
        var password = passwordEntry.Text;
        await _viewModel.Login(email, password);
    }
}