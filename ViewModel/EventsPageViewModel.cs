using MvvmHelpers;
using LocalBusinessExplorer.Entities;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace LocalBusinessExplorer.ViewModel
{

    public class EventsPagetViewModel : BaseViewModel
    {
        private readonly EventDataService _eventDataService;

        public ObservableCollection<EventResult> Events { get; set; }

        public EventsPagetViewModel(EventDataService eventDataService)
        {
            _eventDataService = eventDataService;

            // Initialize the collection with events from the service
            Events = new ObservableCollection<EventResult>(_eventDataService.EventList);
        }
    }

}
