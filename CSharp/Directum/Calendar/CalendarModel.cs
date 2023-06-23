using System.Collections.Generic;

namespace Directum
{
    public class CalendarModel
    {
        public List<MeetingModel> Meetings { get; set; }

        public CalendarModel()
        {
            Meetings = new List<MeetingModel>();

        }
    }
}