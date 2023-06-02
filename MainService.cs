using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;

namespace UltraViewerDestroyer
{
    public partial class MainService : ServiceBase
    {
        private ManualResetEvent stopSignal = new ManualResetEvent(false);
        private Thread workerThread;

        public MainService()
        {
            InitializeComponent();
            //StreamWriter w = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + "log.txt");
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            workerThread = new Thread(Execute);
            workerThread.Start();
        }

        protected override void OnStop()
        {
            stopSignal.Set();
            workerThread.Join();
        }

        private void Execute()
        {
            if (IsValorantRunning()) foreach (Process p in GetUltraViewerProgress()) p.Kill();
            Thread.Sleep(50);
        }

        private bool IsValorantRunning()
        {
            Process[] processes = Process.GetProcessesByName("VALORANT");
            return processes.Length > 0;
        }

        private Process[] GetUltraViewerProgress()
        {
            return Process.GetProcessesByName("UltraViewer_Desktop");
        }
    }
}
