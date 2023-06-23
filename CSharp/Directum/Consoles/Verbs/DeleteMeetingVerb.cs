using CommandLine;

namespace Directum.Consoles.Verbs
{
    [Verb("delete", HelpText = "Delete Meeting by Id", Hidden = false)]

    public class DeleteMeetingVerb
    {        
        [Option('c', "currentStart", Required = true, HelpText = "Current Start Date and Time Meeting in format:'dd.mm.yyyy_hh:mm")]
        public string CurrentStartDateTime { get; set; }
    }
}