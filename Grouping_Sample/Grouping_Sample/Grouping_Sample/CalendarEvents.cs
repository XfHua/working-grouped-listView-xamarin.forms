using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Grouping_Sample
{
    public class CalendarEvents
    {
        //update here: here should be List<EventsHBTwo>
        public List<EventsHBTwo> eventsHB { get; set; }
    }

    //update use this one to shou grouped list data
    public class EventsHB : ObservableCollection<EventTO>
    {
        public string month { get; set; }
        public EventTO eventTO { get; set; }
    }

    //Update: use this one to deserialize json
    public class EventsHBTwo
    {
        public string month { get; set; }
        public EventTO eventTO { get; set; }
    }

    public class EventTO
    {
        public string calendarEventId { get; set; }
        public string title { get; set; }
        public long startDate { get; set; }
        public string startTime { get; set; }
        public bool isStartTimeNull
        {
            set { }
            get
            {
                if (string.IsNullOrEmpty(startTime))
                    return false;
                else return true;
            }
        }
    }
}
