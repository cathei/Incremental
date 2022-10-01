// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Cathei.Mathematics.Benchmarks;

BenchmarkRunner.Run<ArithmeticBenchmark>();

// summaries.Add(BenchmarkRunner.Run<AdditionBenchmark>());
// summaries.Add(BenchmarkRunner.Run<SubtractionBenchmark>());
// summaries.Add(BenchmarkRunner.Run<MultiplicationBenchmark>());
// summaries.Add(BenchmarkRunner.Run<DivisionBenchmark>());
