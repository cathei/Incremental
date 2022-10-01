// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using BenchmarkDotNet.Attributes;
using BreakInfinity;

namespace Cathei.Mathematics.Benchmarks;

[ShortRunJob]
public class ArithmeticBenchmark
{
    public IEnumerable<object[]> DoubleArguments()
    {
        yield return new object[] { 0.0, 1.0 };
        yield return new object[] { 3.141592e+10, 2.45290e+8 };
        yield return new object[] { 0.00015, 0.000000001325 };
        yield return new object[] { 90.12308590830902345, 72.3499590238902103 };
    }

    public IEnumerable<object[]> DecimalArguments()
        => DoubleArguments().Select(x => x.Select(v => (object)(decimal)(double)v).ToArray());

    public IEnumerable<object[]> IncrementalArguments()
        => DoubleArguments().Select(x => x.Select(v => (object)(Incremental)(double)v).ToArray());

    public IEnumerable<object[]> BigDoubleArguments()
        => DoubleArguments().Select(x => x.Select(v => (object)(BigDouble)(double)v).ToArray());
}

public class AdditionBenchmark : ArithmeticBenchmark
{
    [Benchmark]
    [ArgumentsSource(nameof(DoubleArguments))]
    public double DoubleAdd(double doubleA, double doubleB) => doubleA + doubleB;

    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(DecimalArguments))]
    public decimal DecimalAdd(decimal decimalA, decimal decimalB) => decimalA + decimalB;

    [Benchmark]
    [ArgumentsSource(nameof(IncrementalArguments))]
    public Incremental IncrementalAdd(Incremental incrementalA, Incremental incrementalB) => incrementalA + incrementalB;

    [Benchmark]
    [ArgumentsSource(nameof(BigDoubleArguments))]
    public BigDouble BigDoubleAdd(BigDouble bigDoubleA, BigDouble bigDoubleB) => bigDoubleA + bigDoubleB;
}

public class SubtractionBenchmark : ArithmeticBenchmark
{
    [Benchmark]
    [ArgumentsSource(nameof(DoubleArguments))]
    public double DoubleSub(double doubleA, double doubleB) => doubleA - doubleB;

    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(DecimalArguments))]
    public decimal DecimalSub(decimal decimalA, decimal decimalB) => decimalA - decimalB;

    [Benchmark]
    [ArgumentsSource(nameof(IncrementalArguments))]
    public Incremental IncrementalSub(Incremental incrementalA, Incremental incrementalB) => incrementalA - incrementalB;

    [Benchmark]
    [ArgumentsSource(nameof(BigDoubleArguments))]
    public BigDouble BigDoubleSub(BigDouble bigDoubleA, BigDouble bigDoubleB) => bigDoubleA - bigDoubleB;
}

public class MultiplicationBenchmark : ArithmeticBenchmark
{
    [Benchmark]
    [ArgumentsSource(nameof(DoubleArguments))]
    public double DoubleMul(double doubleA, double doubleB) => doubleA * doubleB;

    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(DecimalArguments))]
    public decimal DecimalMul(decimal decimalA, decimal decimalB) => decimalA * decimalB;

    [Benchmark]
    [ArgumentsSource(nameof(IncrementalArguments))]
    public Incremental IncrementalMul(Incremental incrementalA, Incremental incrementalB) => incrementalA * incrementalB;

    [Benchmark]
    [ArgumentsSource(nameof(BigDoubleArguments))]
    public BigDouble BigDoubleMul(BigDouble bigDoubleA, BigDouble bigDoubleB) => bigDoubleA * bigDoubleB;
}

public class DivisionBenchmark : ArithmeticBenchmark
{
    [Benchmark]
    [ArgumentsSource(nameof(DoubleArguments))]
    public double DoubleDiv(double doubleA, double doubleB) => doubleA / doubleB;

    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(DecimalArguments))]
    public decimal DecimalDiv(decimal decimalA, decimal decimalB) => decimalA / decimalB;

    [Benchmark]
    [ArgumentsSource(nameof(IncrementalArguments))]
    public Incremental IncrementalDiv(Incremental incrementalA, Incremental incrementalB) => incrementalA / incrementalB;

    [Benchmark]
    [ArgumentsSource(nameof(BigDoubleArguments))]
    public BigDouble BigDoubleDiv(BigDouble bigDoubleA, BigDouble bigDoubleB) => bigDoubleA / bigDoubleB;
}

