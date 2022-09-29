// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using BenchmarkDotNet.Running;
using Cathei.Mathematics.Benchmarks;

var summary = BenchmarkRunner.Run<ArithmeticBenchmark>();
