// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using BenchmarkDotNet.Attributes;
using BreakInfinity;

namespace Cathei.Mathematics.Benchmarks;

[ShortRunJob]
// [DryJob]
// [SimpleJob(invocationCount: 1000000000)]
[MarkdownExporterAttribute.GitHub]
public abstract class ArithmeticBenchmark
{
    public struct Number
    {
        public readonly double asDouble;
        public readonly decimal asDecimal;
        public readonly Incremental asIncremental;
        public readonly BigDouble asBigDouble;

        public Number(double original)
        {
            asDouble = original;
            asDecimal = (decimal)original;
            asIncremental = (Incremental)original;
            asBigDouble = (BigDouble)original;
        }

        public override string ToString() => asDouble.ToString();
    }

    public IEnumerable<object[]> NumberArguments()
    {
        yield return new object[] { new Number(0.0), new Number(1.0) };
        yield return new object[] { new Number(3.141592e+10), new Number(2.45290e+8) };
        yield return new object[] { new Number(0.00015), new Number(0.000000001325) };
        yield return new object[] { new Number(90.12308590830902345), new Number(72.3499590238902103) };
        yield return new object[] { new Number(0.02), new Number(5050) };
    }
}

public class AddBenchmark : ArithmeticBenchmark
{
    // [Benchmark]
    // [ArgumentsSource(nameof(NumberArguments))]
    // public double DoubleAdd(Number numberA, Number numberB) => numberA.asDouble + numberB.asDouble;

    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(NumberArguments))]
    public Incremental IncrementalAdd(Number numberA, Number numberB) => numberA.asIncremental + numberB.asIncremental;

    [Benchmark]
    [ArgumentsSource(nameof(NumberArguments))]
    public decimal DecimalAdd(Number numberA, Number numberB) => numberA.asDecimal + numberB.asDecimal;

    [Benchmark]
    [ArgumentsSource(nameof(NumberArguments))]
    public BigDouble BigDoubleAdd(Number numberA, Number numberB) => numberA.asBigDouble + numberB.asBigDouble;
}

public class SubtractBenchmark : ArithmeticBenchmark
{
    // [Benchmark]
    // [ArgumentsSource(nameof(NumberArguments))]
    // public double DoubleSub(Number numberA, Number numberB) => numberA.asDouble - numberB.asDouble;

    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(NumberArguments))]
    public Incremental IncrementalSub(Number numberA, Number numberB) => numberA.asIncremental - numberB.asIncremental;

    [Benchmark]
    [ArgumentsSource(nameof(NumberArguments))]
    public decimal DecimalSub(Number numberA, Number numberB) => numberA.asDecimal - numberB.asDecimal;

    [Benchmark]
    [ArgumentsSource(nameof(NumberArguments))]
    public BigDouble BigDoubleSub(Number numberA, Number numberB) => numberA.asBigDouble - numberB.asBigDouble;
}

public class MultiplyBenchmark : ArithmeticBenchmark
{
    // [Benchmark]
    // [ArgumentsSource(nameof(NumberArguments))]
    // public double DoubleMul(Number numberA, Number numberB) => numberA.asDouble * numberB.asDouble;

    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(NumberArguments))]
    public Incremental IncrementalMul(Number numberA, Number numberB) => numberA.asIncremental * numberB.asIncremental;

    [Benchmark]
    [ArgumentsSource(nameof(NumberArguments))]
    public decimal DecimalMul(Number numberA, Number numberB) => numberA.asDecimal * numberB.asDecimal;

    [Benchmark]
    [ArgumentsSource(nameof(NumberArguments))]
    public BigDouble BigDoubleMul(Number numberA, Number numberB) => numberA.asBigDouble * numberB.asBigDouble;
}

public class DivideBenchmark : ArithmeticBenchmark
{
    // [Benchmark]
    // [ArgumentsSource(nameof(NumberArguments))]
    // public double DoubleDiv(Number numberA, Number numberB) => numberA.asDouble / numberB.asDouble;

    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(NumberArguments))]
    public Incremental IncrementalDiv(Number numberA, Number numberB) => numberA.asIncremental / numberB.asIncremental;

    [Benchmark]
    [ArgumentsSource(nameof(NumberArguments))]
    public decimal DecimalDiv(Number numberA, Number numberB) => numberA.asDecimal / numberB.asDecimal;

    [Benchmark]
    [ArgumentsSource(nameof(NumberArguments))]
    public BigDouble BigDoubleDiv(Number numberA, Number numberB) => numberA.asBigDouble / numberB.asBigDouble;
}