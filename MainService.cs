using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;

namespace UltraViewerDestroyer
{
    public partial class MainService : ServiceBase
    {
        private readonly ManualResetEvent stopSignal = new ManualResetEvent(false);
        private readonly Thread worker;

        public MainService()
        {
            InitializeComponent();
            worker = new Thread(Execute);
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            worker.Start();
        }

        protected override void OnStop()
        {
            stopSignal.Set();
            worker.Join();
        }

        private void Execute()
        {
            while (true)
            {
                if (IsValorantRunning()) foreach (Process p in GetUltraViewerProgress()) p.Kill();
                Thread.Sleep(50);
            }
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
