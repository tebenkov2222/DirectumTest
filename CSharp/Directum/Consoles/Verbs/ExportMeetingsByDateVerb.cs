using CommandLine;

namespace Directum.Consoles.Verbs
{
    [Verb("export", HelpText = "Export All Meetings by date to file", Hidden = false)]
    public class ExportMeetingsByDateVerb
    {
        [Option('d', "date", Required = true, HelpText = "Date in format:'dd.mm.yyyy'")]

        public string DateTime{ get; set; }
    }
    
}