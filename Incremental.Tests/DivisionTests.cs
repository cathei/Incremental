// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Cathei.Mathematics.Tests;

public class DivisionTests
{
    public static IEnumerable<Incremental[]> DivisionTestData = new List<Incremental[]>
    {
        new Incremental[] { 0, 1, 0 },
        new Incremental[] { 10, 1, 10 },
        new Incremental[] { 150, 92, 1.630_434_7m },
        new Incremental[] { 2020, 19_940_822, new(10_129_973_500_000_000, -4) },
        new Incremental[] { 1_234_567_890, 2020, new(61_117_222_000_000_000, 5) },
        new Incremental[] { 9_876_547_890, 10, 987_654_789 },
        new Incremental[] { 555_555_555, 44_444_444, 12.500_000_10m },
        new Incremental[] { 0.000_001m, 0.000_1m, 0.01m },
        new Incremental[] { 0.123_456m, 0.333m, 0.370_738_73m },
        new Incremental[] { 10.03m, 2.08m, 4.822_115_3m },
        new Incremental[] { 99.999m, 11.111m, 9 },
    };

    [TestCaseSource(nameof(DivisionTestData))]
    public void TestDivision(Incremental a, Incremental b, Incremental result)
    {
        Assert.AreEqual(result, a / b);
        Assert.AreEqual(result, Incremental.Divide(a, b));
    }

    [Test]
    public void TestDivideByZero()
    {
        Assert.Throws<DivideByZeroException>(() =>
            Incremental.Divide(Incremental.One, Incremental.Zero));
    }
}