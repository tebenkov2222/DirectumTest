using CommandLine;

namespace Directum.Consoles.Verbs
{
    [Verb("get", HelpText = "Get All Meetings by date", Hidden = false)]
    public class GetMeetingsByDateVerb
    {
        [Option('d', "date", Required = true, HelpText = "Date in format:'dd.mm.yyyy'")]

        public string DateTime{ get; set; }

    }
}