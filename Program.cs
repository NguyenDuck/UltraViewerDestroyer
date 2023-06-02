using System.ServiceProcess;
using System.Threading;

namespace UltraViewerDestroyer
{
    static class Program
    {
        static void Main()
        {
#if DEBUG
            MainService service = new MainService();
            service.OnDebug();
            Thread.Sleep(Timeout.Infinite);
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]{new MainService()};
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
