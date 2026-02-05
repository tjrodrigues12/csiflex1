using CSIFLEX.Server.Library;
using CSIFLEX.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace CSIFLEX.Reporting.Service
{
    public class MainService
    {
        CancellationTokenSource source;

        ReportsGenerator generator;

        public void Start()
        {
            Log.Instance.Init("CSIFLEXReporting");
            Log.Info("Reporting Service Started");
            Log.Info($"========================================================================================");

            generator = new ReportsGenerator();

            source = new CancellationTokenSource();

            var token = source.Token;

            var task = Task.Run(() => generator.StartAutoReporting(token), token);
        }

        public void Stop()
        {
            Log.Info("Service Stopped");
            source.Cancel();
        }

        public void Pause()
        {
            Log.Info("Service Paused");
        }
    }
}
