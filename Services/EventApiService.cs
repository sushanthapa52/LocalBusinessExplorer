using LocalBusinessExplorer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LocalBusinessExplorer.Services
{
    public  class EventApiService
    {
        private readonly HttpClient _httpClient;

        public EventApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<EventResult>> GetEventsAsync(string location)
        {
            // Construct the URL with the provided location
            string url1 = $"https://https://eventfunction20241007192532.azurewebsites.net//api/GetEvents?location={Uri.EscapeDataString(location)}";
            string url = $"https://eventfunction20241007192532.azurewebsites.net/api/GetEvents?code=XgiWTCXa18G2qH5BRHNJyeisjdY9K-DEz67MYOr5ame2AzFudG_Dhw%3D%3D";
            try
            {
                // Send the GET request
                var response = await _httpClient.GetAsync(url);

                // Ensure the response is successful
                response.EnsureSuccessStatusCode();

                // Deserialize the JSON response to a list of EventResult
                var events = await response.Content.ReadFromJsonAsync<List<EventResult>>();
                return events;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching events: {ex.Message}");
                return new List<EventResult>();
            }
        }
    }

   
}
