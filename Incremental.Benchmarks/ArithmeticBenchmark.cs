// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using BenchmarkDotNet.Attributes;
using BreakInfinity;

namespace Cathei.Mathematics.Benchmarks;

// [DryJob]
[ShortRunJob]
public class ArithmeticBenchmark
{
    private readonly double doubleA;
    private readonly double doubleB;

    private readonly decimal decimalA;
    private readonly decimal decimalB;

    private readonly Incremental incrementalA;
    private readonly Incremental incrementalB;

    private readonly BigDouble bigDoubleA;
    private readonly BigDouble bigDoubleB;

    private static readonly Random Rng = new Random();

    public ArithmeticBenchmark()
    {
        doubleA = Rng.NextDouble() * 2000000 - 1000000;
        doubleB = Rng.NextDouble() * 2000000 - 1000000;

        decimalA = (decimal)doubleA;
        decimalB = (decimal)doubleB;

        incrementalA = (Incremental)doubleA;
        incrementalB = (Incremental)doubleB;

        bigDoubleA = doubleA;
        bigDoubleB = doubleB;
    }

    [Benchmark]
    public double DoubleAdd() => doubleA + doubleB;

    [Benchmark]
    public decimal DecimalAdd() => decimalA + decimalB;

    [Benchmark]
    public Incremental IncrementalAdd() => incrementalA + incrementalB;

    [Benchmark]
    public BigDouble BigDoubleAdd() => bigDoubleA + bigDoubleB;
    
    [Benchmark]
    public double DoubleSub() => doubleA - doubleB;

    [Benchmark]
    public decimal DecimalSub() => decimalA - decimalB;

    [Benchmark]
    public Incremental IncrementalSub() => incrementalA - incrementalB;

    [Benchmark]
    public BigDouble BigDoubleSub() => bigDoubleA - bigDoubleB;

    [Benchmark]
    public double DoubleMul() => doubleA * doubleB;

    [Benchmark]
    public decimal DecimalMul() => decimalA * decimalB;

    [Benchmark]
    public Incremental IncrementalMul() => incrementalA * incrementalB;

    [Benchmark]
    public BigDouble BigDoubleMul() => bigDoubleA * bigDoubleB;

    [Benchmark]
    public double DoubleDiv() => doubleA / doubleB;

    [Benchmark]
    public decimal DecimalDiv() => decimalA / decimalB;

    [Benchmark]
    public Incremental IncrementalDiv() => incrementalA / incrementalB;

    [Benchmark]
    public BigDouble BigDoubleDiv() => bigDoubleA / bigDoubleB;
}

