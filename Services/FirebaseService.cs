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

        public FirebaseService(IConfiguration configuration, FirebaseAuthClient authClient)
        {
            _configuration = configuration;
            _authClient = authClient;
        }

        //public void InitializeFirebase()
        //{
        //    var apiKey = _configuration["Firebase:ApiKey"];
        //    var authDomain = _configuration["Firebase:AuthDomain"];
        //    var projectId = _configuration["Firebase:ProjectId"];
        //    var storageBucket = _configuration["Firebase:StorageBucket"];
        //    var messagingSenderId = _configuration["Firebase:MessagingSenderId"];
        //    var appId = _configuration["Firebase:AppId"];
        //    var measurementId = _configuration["Firebase:MeasurementId"];

        //}
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


    }
}
