using System;
using System.Globalization;
using Directum.Calendar;
using Directum.Consoles.Verbs;
using Directum.Core;

namespace Directum.Consoles
{
    public class ConsoleCommandHandler
    {
        private readonly CalendarController _calendarController;

        public ConsoleCommandHandler(CalendarController calendarController)
        {
            _calendarController = calendarController;
        }
        public int AddMeeting(AddMeetingConsoleVerb opts)
        {
            if (!opts.StartDateTime.TryDateTimeParse(out var startDateTime))
            {
                Console.WriteLine("Start DateTime format parse error. Format: dd.mm.yyyy_hh:mm");
                return 1;
            }
            if (!opts.Interval.TryTimeSpanParse( out var interval))
            {
                Console.WriteLine("Interval format parse error. Format: hh:mm");
                return 1;
            }

            TimeSpan reminder = default;
            if (opts.ReminderDateTime != null && !opts.ReminderDateTime.TryTimeSpanParse(out reminder))
            {
                Console.WriteLine("Reminder format parse error. Format: hh:mm");
                return 1;
            }

            _calendarController.AddMeeting(opts.Name, startDateTime, interval, reminder);
            return 0;
        }
        public int UpdateMeeting(UpdateMeetingVerb opts)
        {
            if (!opts.CurrentStartDateTime.TryDateTimeParse(out var curStartDateTime))
            {
                Console.WriteLine("Start DateTime format parse error. Format: dd.mm.yyyy_hh:mm");
                return 1;
            }
            //MeetingModel meetingModel;
            if (!_calendarController.TryGetMeeting(curStartDateTime, out var meetingModel ))
            {
                Console.WriteLine("Key not found");
                return 1;
            }

            var newModel = new MeetingModel();
            newModel.Name = meetingModel.Name;
            newModel.StartDateTime = meetingModel.StartDateTime;
            newModel.EndDateTime = meetingModel.EndDateTime;
            newModel.ReminderDateTime = meetingModel.ReminderDateTime;
            newModel.Alarmed = meetingModel.Alarmed;
            if (opts.StartDateTime != null)
            {
                if (!opts.StartDateTime.TryDateTimeParse(out var startDateTime))
                {
                    Console.WriteLine("Start DateTime format parse error. Format: dd.mm.yyyy_hh:mm");
                }
                else
                {
                    var intervalEnd = meetingModel.EndDateTime.Subtract(meetingModel.StartDateTime);
                    var intervalRemind = meetingModel.ReminderDateTime.Subtract(meetingModel.StartDateTime);
                    newModel.StartDateTime = startDateTime;
                    newModel.EndDateTime = startDateTime + intervalEnd;
                    if(newModel.ReminderDateTime != default) newModel.ReminderDateTime = startDateTime - intervalRemind;
                    
                }
            }

            if (opts.Interval != null)
            {
                if (!opts.Interval.TryTimeSpanParse( out var interval))
                {
                    Console.WriteLine("Interval format parse error. Format: hh:mm");
                }
                else
                {
                    newModel.EndDateTime = newModel.StartDateTime + interval;
                }
            }
            
            if (opts.ReminderDateTime != null)
            {
                if (!opts.ReminderDateTime.TryTimeSpanParse( out var interval))
                {
                    Console.WriteLine("Interval format parse error. Format: hh:mm");
                }
                else
                {
                    newModel.ReminderDateTime = newModel.StartDateTime - interval;
                }
            }

            _calendarController.UpdateMeeting(meetingModel, newModel);
            return 0;
        }
        public int GetMeeting(GetMeetingsByDateVerb opts)
        {
            if (!DateTime.TryParseExact(opts.DateTime, "d.M.yyyy",null, DateTimeStyles.None, out var dateTime))
            {
                Console.WriteLine("DateTime format parse error. Format: dd.mm.yyyy");
                return 1;
            }
            _calendarController.ViewAllMeetingsByDate(dateTime);

            return 0;
        }
        public int DeleteMeeting(DeleteMeetingVerb opts)
        {
            if (!opts.CurrentStartDateTime.TryDateTimeParse(out var curStartDateTime))
            {
                Console.WriteLine("Start DateTime format parse error. Format: dd.mm.yyyy_hh:mm");
                return 1;
            }
            if (!_calendarController.TryDelete(curStartDateTime))
            {
                Console.WriteLine("Key not found");
                return 1;
            }
            Console.WriteLine($"Success delete meeting by time {curStartDateTime}");
            
            return 0;
        }

        public int Export(ExportMeetingsByDateVerb opts)
        {
            if (!DateTime.TryParseExact(opts.DateTime, "d.M.yyyy",null, DateTimeStyles.None, out var dateTime))
            {
                Console.WriteLine("DateTime format parse error. Format: dd.mm.yyyy");
                return 1;
            }
            _calendarController.Export(dateTime);
            return 0;

        }
    }
}