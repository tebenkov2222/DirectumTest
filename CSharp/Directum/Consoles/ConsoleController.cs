using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Globalization;
using CommandLine;
using Directum.Consoles.Verbs;
using Directum.Core.Threads;
using Parser = System.CommandLine.Parsing.Parser;

namespace Directum.Consoles
{
    public class ConsoleController: IThreadHandler
    {
        private ConsoleCommandHandler _consoleCommandHandler;
        public event Action Canceled;

        public ConsoleController(ConsoleCommandHandler consoleCommandHandler)
        {
            _consoleCommandHandler = consoleCommandHandler;
        }

        public void Enable()
        {
            Console.CancelKeyPress+=ConsoleOnCancelKeyPress;
        }

        public void Update()
        {
            WaitInput();
        }

        private void WaitInput()
        {
            try
            {
                string input = Console.ReadLine();
                var args = CommandLineStringSplitter.Instance.Split(input);
                CommandLine.Parser.Default.ParseArguments<AddMeetingConsoleVerb, GetMeetingsByDateVerb, DeleteMeetingVerb, UpdateMeetingVerb, ExportMeetingsByDateVerb>(args)
                    .MapResult(
                        (AddMeetingConsoleVerb opts) => _consoleCommandHandler.AddMeeting(opts),
                        (GetMeetingsByDateVerb opts) => _consoleCommandHandler.GetMeeting(opts),
                        (DeleteMeetingVerb opts) => _consoleCommandHandler.DeleteMeeting(opts),
                        (UpdateMeetingVerb opts) => _consoleCommandHandler.UpdateMeeting(opts),
                        (ExportMeetingsByDateVerb opts) => _consoleCommandHandler.Export(opts),
                        errs => 1);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Operation was cancelled.");
            }
        }
        public void Disable()
        {
            Console.CancelKeyPress-=ConsoleOnCancelKeyPress;
        }

        private void ConsoleOnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Canceled?.Invoke();
        }
    }
}