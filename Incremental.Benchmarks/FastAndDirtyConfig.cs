// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using Perfolizer.Horology;

public class FastAndDirtyConfig : ManualConfig
{
    public FastAndDirtyConfig()
    {
        AddJob(Job.Default
            .WithLaunchCount(5)
            .WithIterationTime(TimeInterval.FromMilliseconds(100))
            .WithWarmupCount(3)
        );
    }
}
