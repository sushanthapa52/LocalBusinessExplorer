using Firebase.Database;
using LocalBusinessExplorer.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalBusinessExplorer.Services
{
    public class FirebaseDb
    {
        public FirebaseDb( ) {
        }

        // Fetch events from Firebase Realtime Database
        public async Task<List<EventResult>>  EventResults()
         {
            var firebaseUrl = "https://lbecapstone-default-rtdb.firebaseio.com/"; 
            var firebaseAuth = "AIzaSyDPpT0BibZkokcLeDYf_J5kocRyNbuhVC4";
            var firebaseClient = new FirebaseClient(
                firebaseUrl,
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(firebaseAuth)
                });

            var events = await firebaseClient
            .Child("events")
            .OnceAsync<EventResult>();

        // Map Firebase records to your EventResult model
        return events.Select(eventRecord => new EventResult
        {
            Title = eventRecord.Object.Title,
            StartDate = eventRecord.Object.StartDate,
            Address = eventRecord.Object.Address
        }).ToList();
    }
    }

}
