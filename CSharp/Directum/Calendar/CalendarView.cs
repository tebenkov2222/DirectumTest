using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Directum.Calendar
{
    public class CalendarView
    {
        public void ViewMeetingList(IEnumerable<MeetingModel> meetings)
        {
            ViewTableRow(@"Name|Start|End|Reminder");
            foreach (var meeting in meetings)
            {
                ViewTableRow(@$"{meeting.Name}|{meeting.StartDateTime.ToString("g")}|{meeting.EndDateTime.ToString("g")}|{(meeting.ReminderDateTime != default ? meeting.ReminderDateTime.ToString("g") : '-')}");
            }
        }

        private void ViewTableRow(string input)
        {
            string delimited = @"(.+)[\t\u007c](.+)[\t\u007c](.+)[\t\u007c](.+)";
            foreach (Match match in Regex.Matches(input, delimited))
                Console.WriteLine("{0,-20} {1,-20} {2,-20} {3,-20}", 
                    match.Groups[1].Value,
                    match.Groups[2].Value,
                    match.Groups[3].Value,
                    match.Groups[4].Value);
        }
    }
}