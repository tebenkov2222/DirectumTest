using System;

namespace Directum
{
    public class MeetingModel
    {
        public string Name { get; set; }
        public DateTime StartDateTime{ get; set; }
        public DateTime EndDateTime{ get; set; }
        public DateTime ReminderDateTime{ get; set; }
        public bool Alarmed{ get; set; }
    }
}