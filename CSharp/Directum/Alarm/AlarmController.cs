using System;
using System.Threading;
using Directum.Core.Threads;

namespace Directum.Alarm
{
    public class AlarmController: IThreadHandler
    {
        public event Action<MeetingModel> Elapsed; 
        private CalendarModel _calendarModel;

        public AlarmController(CalendarModel calendarModel)
        {
            _calendarModel = calendarModel;
        }

        public void Enable()
        {
            
        }

        public void Update()
        {
            foreach (var meeting in _calendarModel.Meetings)
            {
                if (!meeting.Alarmed && meeting.ReminderDateTime.Subtract(DateTime.Now) < new TimeSpan(0, 0, 1))
                {
                    meeting.Alarmed = true;
                    Elapsed?.Invoke(meeting);
                }
            }
        }

        public void Disable()
        {
            
        }
    }
}