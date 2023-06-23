using CommandLine;

namespace Directum.Consoles.Verbs
{
    [Verb("add", HelpText = "Add Meeting to shedule.", Hidden = false)]
    public class AddMeetingConsoleVerb
    {
        [Option('n', "name", Required = true, HelpText = "Name Meeting")]
        public string Name { get; set; }
        [Option('s', "start", Required = true, HelpText = "Start Date and Time Meeting in format:'dd.mm.yyyy_hh:mm'")]
        public string StartDateTime{ get; set; }
        [Option('i', "interval", Required = true, HelpText = "Meeting interval in format: 'hh:mm'. Max: 24h")]
        public string Interval{ get; set; }
        [Option('r', "reminder", Required = false, HelpText = "The time period for which you need to be reminded of the meeting in format: 'hh:mm'. Max: 24h")]
        public string ReminderDateTime{ get; set; }
    }
}