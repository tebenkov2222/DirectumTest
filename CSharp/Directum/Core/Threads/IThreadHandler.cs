namespace Directum.Core.Threads
{
    public interface IThreadHandler
    {
        public void Enable();
        public void Update();
        public void Disable();
    }
}