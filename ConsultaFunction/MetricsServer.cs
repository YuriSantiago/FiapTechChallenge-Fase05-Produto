using Microsoft.Extensions.Hosting;
using Prometheus;

namespace ConsultaFunction
{
    public class MetricsServer : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var server = new KestrelMetricServer(port: 7112); 
            server.Start();

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
