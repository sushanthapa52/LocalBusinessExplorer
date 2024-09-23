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

        public FirebaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void InitializeFirebase()
        {
            var apiKey = _configuration["Firebase:ApiKey"];
            var authDomain = _configuration["Firebase:AuthDomain"];
            var projectId = _configuration["Firebase:ProjectId"];
            var storageBucket = _configuration["Firebase:StorageBucket"];
            var messagingSenderId = _configuration["Firebase:MessagingSenderId"];
            var appId = _configuration["Firebase:AppId"];
            var measurementId = _configuration["Firebase:MeasurementId"];

        }
    }
}
