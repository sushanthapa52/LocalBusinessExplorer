using LocalBusinessExplorer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LocalBusinessExplorer.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {

        private string _email;
        private string _password;
        private string _loginResult;
        private readonly FirebaseService _firebaseService;
        public LoginViewModel(FirebaseService firebaseService)
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

        public string LoginResult
        {
            get => _loginResult;
            set
            {
                _loginResult = value;
                OnPropertyChanged();
            }
        }
        public async Task Login(string Email, string Password)
        {
            var token = await _firebaseService.LoginWithEmailPassword(Email, Password);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
