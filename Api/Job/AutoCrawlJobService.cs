
using Application.IJob;
using Infrastructure.ImplimentJob;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Api.Job
{
    public sealed class AutoCrawlJobService : BackgroundService
    {
        private readonly IServiceScopeFactory _sp;
        private readonly ILogger<IAutoCrawlJob> _log;
        private  AutoCrawlJobOption _opt;
        private readonly SemaphoreSlim _gate = new(1, 1);
       
        public AutoCrawlJobService(IServiceScopeFactory sp, ILogger<IAutoCrawlJob> log, AutoCrawlJobOption opt)
        {
            _sp = sp;
            _log = log;
            _opt = opt;
            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _log.LogInformation("MyJobHostedService started. Interval={Interval}s, RunOnStart={RunOnStart}",
                _opt.IntervalSeconds, _opt.RunOnStart);
                

                // .NET 6+ PeriodicTimer: ổn định, không lệch nhịp

                using var timer = new PeriodicTimer(TimeSpan.FromSeconds(_opt.IntervalSeconds));

                try
                {
                    if (_opt.RunOnStart)
                        await RunOnceSafeAsync(stoppingToken);
                    while (await timer.WaitForNextTickAsync(stoppingToken))
                    {

                        await RunOnceSafeAsync(stoppingToken);
                        _log.LogInformation("Delay: Đang chờ chạy lại...");
                        await Task.Delay(5000, stoppingToken);
                    }

                }
                catch (OperationCanceledException) { /* app stopping */ }
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "AutoCrawlJobService - Error: ");
            }
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
                using var scope1 = _sp.CreateScope();
                IAutoCrawlJob job1 = scope1.ServiceProvider.GetRequiredService<IAutoCrawlJob>();
                await job1.ExecuteAsync(ct);

                //await Task.WhenAll(t1, t2);
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
