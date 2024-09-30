using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalBusinessExplorer.Services
{
    public class GooglePlaceAPI
    {
        private readonly IConfigurationSection _googleConfig;
        private readonly string _apiKey;
        private readonly string _clientId;

        public GooglePlaceAPI(IConfigurationSection googleConfig)
        {
            _googleConfig = googleConfig;

            // Fetch Google ApiKey and ClientId from configuration
            _apiKey = _googleConfig["ApiKey"];
        }
        public string GetGoogleApiKey()
        {
            return _apiKey;
        }



    }
}
