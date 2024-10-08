using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SerpApi;
using System.Collections;
using Firebase.Database;
using Firebase.Database.Query;

namespace EventScript
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var firebaseUrl = "https://lbecapstone-default-rtdb.firebaseio.com/"; // Replace with your Firebase Realtime Database URL
            var firebaseAuth = "AIzaSyDPpT0BibZkokcLeDYf_J5kocRyNbuhVC4"; // Replace with your Firebase Database secret (or use the API key if enabled)
            var firebaseClient = new FirebaseClient(
                firebaseUrl,
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(firebaseAuth)
                });


            // Retrieve the API key from configuration
            string apiKey = "8f5bf0a8e0738550c16a52280e882e72760346de7e2525dbf8d5e14be9824fcb";

            // Ask the user for input
            Console.WriteLine("Please enter a location for the event search:");
            string location = Console.ReadLine(); // Get user input for the location

            if (string.IsNullOrWhiteSpace(location))
            {
                Console.WriteLine("Location cannot be empty. Please enter a valid location.");
                return; // Exit the application if location is invalid
            }
            // Test the event fetching with a location (for example, "Toronto")
            List<EventResult> events = await GetEventsAsync(location, apiKey);



            // Output the event results
            foreach (var eventResult in events)
            {
                Console.WriteLine($"Title: {eventResult.Title}, Start Date: {eventResult.StartDate}, Address: {eventResult.Address}");

                // Push the event to Firebase
                await firebaseClient
                    .Child("events")
                    .PostAsync(new EventResult
                    {
                        Title = eventResult.Title,
                        StartDate = eventResult.StartDate,
                        Address = eventResult.Address
                    });

                Console.WriteLine($"Event '{eventResult.Title}' added to Firebase.");
            }
        }

        public static async Task<List<EventResult>> GetEventsAsync(string location, string apiKey)
        {
            Hashtable ht = new Hashtable
            {
                { "engine", "google_events" },
                { "q", $"Events in {location}" },
                { "hl", "en" },
                { "gl", "us" }
            };

            GoogleSearch search = new GoogleSearch(ht, apiKey);

            // Get the JSON data
            JObject data = await Task.Run(() => search.GetJson());

   

            var eventsResults = data["events_results"];
            var eventList = new List<EventResult>();

            // Process and display the results
            if (eventsResults != null)
            {
                foreach (var result in eventsResults)
                {
                    // Extracting the required fields
                    string eventTitle = result["title"]?.ToString();
                    string eventStartDate = result["date"]?["start_date"]?.ToString();
                    string eventAddress = result["address"] != null
                        ? string.Join(", ", result["address"].ToObject<List<string>>())
                        : "Address not available";

                    eventList.Add(new EventResult
                    {
                        Title = eventTitle,
                        StartDate = eventStartDate,
                        Address = eventAddress
                    });
                }
            }
            else
            {
                Console.WriteLine("No events found.");
            }

            return eventList;
        }

        public class EventResult
        {
            public string Title { get; set; }
            public string StartDate { get; set; }
            public string Address { get; set; }
        }
    }
}
