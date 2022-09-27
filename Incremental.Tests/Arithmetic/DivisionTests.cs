// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using NUnit.Framework;
using static Cathei.Mathematics.Incremental;

namespace Cathei.Mathematics.Tests;

public class DivisionTests
{
    public static IEnumerable<Incremental[]> DivisionTestData = new List<Incremental[]>
    {
        new Incremental[] { 0, 1, 0 },
        new Incremental[] { 10, 1, 10 },
        new Incremental[] { 150, 92, new(16_304_347_826_086_950, 0) },
        new Incremental[] { 2020, 19_940_822, new(10_129_973_578_822_377, -4) },
        new Incremental[] { 1_234_567_890, 2020, new(61_117_222_277_227_720, 5) },
        new Incremental[] { 9_876_547_890, 10, 987_654_789 },
        new Incremental[] { 555_555_555, 44_444_444, new(12_500_000_112_500_001, 1) },
        new Incremental[] { 0.000_001m, 0.000_1m, 0.01m },
        new Incremental[] { 0.123_456m, 0.333m, new(37_073_873_873_873_870, -1) },
        new Incremental[] { 10.03m, 2.08m, new(48_221_153_846_153_840, 0) },
        new Incremental[] { 99.999m, 11.111m, 9 },
        new Incremental[] { new(Unit, 90), new(Unit, -10), new(Unit, 100) },
        new Incremental[]
        {
            new(9 * Unit, 99),
            new(9 * Unit, 99),
            1
        },
        new Incremental[]
        {
            new(81 * Unit, 198),
            new(9 * Unit, 99),
            new(9 * Unit, 99)
        },
        new Incremental[]
        {
            new(10_000_000_000_000_001, 0),
            1,
            new(10_000_000_000_000_001, 0)
        },
        new Incremental[]
        {
            new(99_999_999_999_999_998, 1),
            new(99_999_999_999_999_999, 0),
            new(99_999_999_999_999_990, 0)
        },
    };

    [TestCaseSource(nameof(DivisionTestData))]
    public void TestDivision(Incremental a, Incremental b, Incremental result)
    {
        Assert.AreEqual(result, a / b);
        Assert.AreEqual(result, Divide(a, b));
    }

    [Test]
    public void TestDivideByZero()
    {
        Assert.Throws<DivideByZeroException>(() =>
            Divide(One, Zero));
    }
}