using LocalBusinessExplorer.Services;
using Microsoft.Maui.ApplicationModel.Communication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LocalBusinessExplorer.ViewModel
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
        private string _email;
        private string _password;
        private string _loginResult;
        private string _confirmpassword;

        private readonly FirebaseService _firebaseService;
        public SignUpViewModel(FirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string ConfirmPassword
        {
            get => _confirmpassword;
            set
            {
                _confirmpassword = value;
                OnPropertyChanged();
            }
        }

        public string LoginResult
        {
            get => _loginResult;
            set
            {
                _loginResult = value;
                OnPropertyChanged();
            }
        }
        public async Task SignUp(string Email, string Password)
        {
            try
            {
                // Assuming this method registers the user and returns a token
                var token = await _firebaseService.RegisterWithEmailPassword(Email, Password);

                // Navigate to the login page after successful registration
                await Shell.Current.GoToAsync("//LoginPage");
            }
            catch (Exception ex)
            {
                // Handle any errors during the sign-up process
                // You might want to display a message to the user
                Console.WriteLine($"Sign up failed: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
