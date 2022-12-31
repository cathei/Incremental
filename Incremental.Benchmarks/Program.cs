// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Cathei.Mathematics.Benchmarks;

var summaries = new List<Summary>();

summaries.Add(BenchmarkRunner.Run<AddBenchmark>());
summaries.Add(BenchmarkRunner.Run<SubtractBenchmark>());
summaries.Add(BenchmarkRunner.Run<MultiplyBenchmark>());
summaries.Add(BenchmarkRunner.Run<DivideBenchmark>());

foreach (var summary in summaries)
{
    MarkdownExporter.GitHub.ExportToLog(summary, ConsoleLogger.Default);
}
