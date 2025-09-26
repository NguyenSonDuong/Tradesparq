
using Application.IJob;
using Infrastructure.ImplimentJob;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Api.Job
{
    public sealed class AutoCrawlJobService : BackgroundService
    {
        private readonly IServiceProvider _sp;
        private readonly ILogger<AutoCrawlJobService> _log;
        private readonly AutoCrawlJobOption _opt;
        private readonly SemaphoreSlim _gate = new(1, 1);
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _log.LogInformation("MyJobHostedService started. Interval={Interval}s, RunOnStart={RunOnStart}",
            _opt.IntervalSeconds, _opt.RunOnStart);

            if (_opt.RunOnStart)
                await RunOnceSafeAsync(stoppingToken);

            // .NET 6+ PeriodicTimer: ổn định, không lệch nhịp
            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(_opt.IntervalSeconds));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                    await RunOnceSafeAsync(stoppingToken);
            }
            catch (OperationCanceledException) { /* app stopping */ }
        }
        private async Task RunOnceSafeAsync(CancellationToken ct)
        {
            if (!await _gate.WaitAsync(0, ct))
            {
                _log.LogWarning("Skip run: previous job still running.");
                return;
            }

            try
            {
                using var scope = _sp.CreateScope();
                var job = scope.ServiceProvider.GetRequiredService<IAutoCrawlJob>();
                await job.ExecuteAsync(ct);
            }
            catch (OperationCanceledException)
            {
                _log.LogInformation("Job canceled.");
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Job failed.");
                // tuỳ chọn: swallow để vòng lặp không chết
            }
            finally
            {
                _gate.Release();
            }
        }
    }
}
