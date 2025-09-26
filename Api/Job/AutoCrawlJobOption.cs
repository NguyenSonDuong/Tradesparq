namespace Api.Job
{
    public sealed class AutoCrawlJobOption
    {
        public int IntervalSeconds { get; init; } = 60;
        public bool RunOnStart { get; init; } = true;
    }
}
