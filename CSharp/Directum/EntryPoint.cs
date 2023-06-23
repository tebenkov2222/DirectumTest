using Directum.Calendar;
using Directum.Consoles;
using Directum.Core.Threads;

namespace Directum
{
    public class EntryPoint
    {
        private readonly CalendarController _calendarController;
        private readonly ThreadController _calendarThreadController;

        private readonly ConsoleCommandHandler _consoleCommandHandler;
        
        private readonly ConsoleController _consoleController;
        private readonly ThreadController _consoleThreadController;

        public EntryPoint()
        {
            _calendarController = new CalendarController();
            _calendarThreadController = new ThreadController(_calendarController);

            _consoleCommandHandler = new ConsoleCommandHandler(_calendarController);
            
            _consoleController = new ConsoleController(_consoleCommandHandler);
            _consoleController.Canceled+=ConsoleControllerOnCanceled;
            _consoleThreadController = new ThreadController(_consoleController);
            
            _calendarThreadController.Start();
            _consoleThreadController.Start();
        }

        private void ConsoleControllerOnCanceled()
        {
            _consoleController.Canceled-=ConsoleControllerOnCanceled;
            _calendarThreadController.Stop();
            _consoleThreadController.Stop();
        }
    }
}