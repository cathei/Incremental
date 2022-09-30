// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using NUnit.Framework;
using static Cathei.Mathematics.Incremental;

namespace Cathei.Mathematics.Tests;

public class MultiplicationTests
{
    public static IEnumerable<Incremental[]> MultiplicationTestData = new List<Incremental[]>
    {
        new Incremental[] { 0, 0, 0 },
        new Incremental[] { 0, 1, 0 },
        new Incremental[] { 10, 1, 10 },
        new Incremental[] { 150, 92, 13800 },
        new Incremental[] { 2020, 19_940_822, 40_280_460_440L },
        new Incremental[] { 1_234_567_890, 0, 0 },
        new Incremental[] { 9_876_547_890L, 10, 98_765_478_900L },
        new Incremental[] { 555_555_555, 44_444_444, new(24_691_357_753_086_420, 16) },
        new Incremental[] { 0.000_001m, 0.000_1m, new(Unit, -10) },
        new Incremental[] { 0.123_456m, 0.333m, new(41_110_848_000_000_000, -2) },
        new Incremental[] { 10.03m, 2.08m, 20.8624m },
        new Incremental[] { 99.999m, 1.111m, 111.098_889m },
        new Incremental[] { new(Unit, 99), 100, new(Unit, 101) },
        new Incremental[]
        {
            new(9 * Unit, 99),
            new(9 * Unit, 99),
            new(81 * Unit, 198)
        },
        new Incremental[]
        {
            new(-9 * Unit, 99),
            new(9 * Unit, 99),
            new(-81 * Unit, 198)
        },
        new Incremental[]
        {
            new(-9 * Unit, 99),
            new(-9 * Unit, 99),
            new(81 * Unit, 198)
        },
        new Incremental[]
        {
            new(10_000_000_000_000_001, 0),
            1,
            new(10_000_000_000_000_001, 0)
        },
        new Incremental[]
        {
            new(10_000_000_000_000_005, 0),
            10,
            new(10_000_000_000_000_005, 1)
        },
        new Incremental[]
        {
            new(10_000_000_700_000_000, 0),
            new(10_000_000_070_000_000, 0),
            new(10_000_000_770_000_005, 0)
        },
        new Incremental[]
        {
            new(99_999_999_999_999_999, 0),
            new(99_999_999_999_999_999, 0),
            new(99_999_999_999_999_998, 1)
        },
    };

    [TestCaseSource(nameof(MultiplicationTestData))]
    public void TestMultiplication(Incremental a, Incremental b, Incremental result)
    {
        Assert.AreEqual(result, a * b);
        Assert.AreEqual(result, b * a);
        Assert.AreEqual(result, Multiply(a, b));
    }
}