using Firebase.Auth;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LocalBusinessExplorer.Services
{
    public class FirebaseService
    {
        private readonly IConfiguration _configuration;
        private readonly FirebaseAuthClient _authClient;
        private readonly string _apiKey;


        public FirebaseService(IConfiguration configuration, FirebaseAuthClient authClient)
        {
            _configuration = configuration;
            _authClient = authClient;

            _apiKey = _configuration.GetSection("Google")["ApiKey"];

        }

        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                var userCredential = await _authClient.SignInWithEmailAndPasswordAsync(email, password);
                //var userID = userCredential.User.Uid;

                return userCredential.User.Uid;
                //return userCredential.User?.GetIdTokenAsync().ToString();
            }
            catch (Exception ex)
            {
                // Handle error
                return null;
            }
        }
        public async Task<string> RegisterWithEmailPassword(string email, string password)
        {
            try
            {
                var userCredential = await _authClient.CreateUserWithEmailAndPasswordAsync(email, password);
                return userCredential.User?.GetIdTokenAsync().ToString();
            }
            catch (Exception ex)
            {
                // Handle error
                return null;
            }
        }


    }
}
