using CommandLine;

namespace Directum.Consoles.Verbs
{
    [Verb("update", HelpText = "Add Meeting to shedule.", Hidden = false)]
    public class UpdateMeetingVerb
    {
        [Option('c', "currentStart", Required = false, HelpText = "Current Start Date and Time Meeting in format:'dd.mm.yyyy_hh:mm'")]
        public string CurrentStartDateTime{ get; set; }
        [Option('n', "name", Required = false, HelpText = "Name Meeting")]
        public string Name { get; set; }
        [Option('s', "start", Required = false, HelpText = "Start Date and Time Meeting in format:'dd.mm.yyyy_hh:mm'")]
        public string StartDateTime{ get; set; }
        [Option('i', "interval", Required = false, HelpText = "Meeting interval in format: 'hh:mm'. Max: 24h")]
        public string Interval{ get; set; }
        [Option('r', "reminder", Required = false, HelpText = "The time period for which you need to be reminded of the meeting in format: 'hh:mm'. Max: 24h")]
        public string ReminderDateTime{ get; set; }
    }
}