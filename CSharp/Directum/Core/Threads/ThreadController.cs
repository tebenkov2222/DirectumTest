using System;
using System.Threading;

namespace Directum.Core.Threads
{
    public class ThreadController
    {
        private IThreadHandler _threadHandler;
        private Thread _thread;
        private bool _isActive;
        private int _deltaTimeMillis;

        public ThreadController(IThreadHandler threadHandler, int deltaTimeMillis = 20)
        {
            _deltaTimeMillis = deltaTimeMillis;
            _threadHandler = threadHandler;
            _thread = new Thread(ThreadUpdate);
        }

        public void Start()
        {
            _isActive = true;
            _thread.Start();
        }

        private void ThreadUpdate()
        {
            _threadHandler.Enable();
            while (_isActive)
            {
                Thread.Sleep(_deltaTimeMillis);
                _threadHandler.Update();
            }
        }
        public void Stop()
        {
            _threadHandler.Disable();
            _isActive = false;
        }
    }
}