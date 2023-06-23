using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using Directum.Alarm;
using Directum.Core.Threads;
using Newtonsoft.Json;

namespace Directum.Calendar
{
    public class CalendarController : IThreadHandler
    {
        private readonly CalendarModel _calendarModel = new CalendarModel();
        private readonly CalendarView _calendarView = new CalendarView();
        private readonly AlarmController _alarmController;
        private readonly ThreadController _alarmThreadController;

        public CalendarController()
        {
            _alarmController = new AlarmController(_calendarModel);
            _alarmThreadController = new ThreadController(_alarmController, 1000);
        }

        public void Enable()
        {
            Console.WriteLine($"Start Calendar");
            _alarmController.Elapsed += AlarmControllerOnElapsed;
            _alarmThreadController.Start();
        }

        public void Update()
        {

        }

        public void Disable()
        {
            _alarmController.Elapsed -= AlarmControllerOnElapsed;
            _alarmThreadController.Stop();

            Console.WriteLine($"Exit from calendar");
        }

        private void AlarmControllerOnElapsed(MeetingModel meeting)
        {
            Console.WriteLine($"Alarm about meeting: {meeting.Name}");
        }

        public void AddMeeting(string name, DateTime startDateTime, TimeSpan interval, TimeSpan reminder)
        {
            if (DateTime.Now > startDateTime)
            {
                Console.WriteLine("Start Data Error. Enter a valid date");
                return;
            }

            var meetingModel = new MeetingModel()
            {
                Name = name,
                StartDateTime = startDateTime,
                EndDateTime = startDateTime + interval,
                ReminderDateTime = reminder != default ? startDateTime - reminder : default,
                Alarmed = reminder == default
            };
            if (CheckIntercept(meetingModel)) return;
            AddNewMeeting(meetingModel);
        }

        public bool CheckIntercept(MeetingModel model)
        {
            foreach (var meeting in _calendarModel.Meetings)
            {
                if (MeetingTools.Intercept(meeting, model))
                {
                    Console.WriteLine(
                        $"Meetings intercepted with {meeting.Name}. Started at {meeting.StartDateTime}. Ended at {meeting.EndDateTime}");
                    return true;
                }
            }

            return false;
        }

        private void AddNewMeeting(MeetingModel meetingModel)
        {
            _calendarModel.Meetings.Add(meetingModel);
            ViewOnceMeetingsByDate(meetingModel);
        }

        private void ViewOnceMeetingsByDate(MeetingModel model)
        {
            _calendarView.ViewMeetingList(new[] { model });
        }

        private IEnumerable<MeetingModel> GetModelsOnDate(DateTime dateTime)
        {
            return _calendarModel.Meetings.Where(m => m.StartDateTime.Date.Equals(dateTime.Date));
        }

    public void ViewAllMeetingsByDate(DateTime dateTime)
        {
            var meetings = GetModelsOnDate(dateTime.Date);
            _calendarView.ViewMeetingList(meetings);
        }

        public bool TryGetMeeting(DateTime startDateTime, out MeetingModel meetingModel)
        {
            meetingModel = _calendarModel.Meetings.FirstOrDefault(m => m.StartDateTime.Equals(startDateTime));
            return meetingModel != null;
        }
        public bool TryDelete(DateTime startDateTime)
        {
            var meetingModel = _calendarModel.Meetings.FirstOrDefault(m => m.StartDateTime.Equals(startDateTime));
            if (meetingModel == null) return false;
            _calendarModel.Meetings.Remove(meetingModel);
            return true;
        }

        public void Export(DateTime dateTime)
        {
            var list = GetModelsOnDate(dateTime.Date).ToList();
            var serializeObject = JsonConvert.SerializeObject(list);
            var directoryPath = "./export";
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
            directoryPath = Path.GetFullPath(directoryPath);
            var fullPath = Path.Combine(directoryPath,$"Export{DateTime.Now.ToString("ddMMyy_HHmm")}.txt");
            File.WriteAllText(fullPath,serializeObject);
            Console.WriteLine($"Exported data to {fullPath}");
        }

        public void UpdateMeeting(MeetingModel meetingModel, MeetingModel newModel)
        {
            _calendarModel.Meetings.Remove(meetingModel);
            if (CheckIntercept(newModel))
            {
                newModel = meetingModel;
            }
            _calendarModel.Meetings.Add(newModel);
            ViewOnceMeetingsByDate(newModel);
        }
    }
}