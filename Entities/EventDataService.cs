using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalBusinessExplorer.Entities
{
    public class EventDataService
    {
        private static EventDataService _instance;
        public static EventDataService Instance => _instance ??= new EventDataService();

        public List<EventResult> EventList { get; set; } = new List<EventResult>();
    }
}
