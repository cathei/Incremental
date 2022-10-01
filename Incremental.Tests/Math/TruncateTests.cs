// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using NUnit.Framework;
using static Cathei.Mathematics.Incremental;

namespace Cathei.Mathematics.Tests;

public class TruncateTests
{
    public static IEnumerable<object[]> TruncateTestData = new List<object[]>
    {
        new object[] { 1m, 0, 1m, 1m, 1m, 1m },
        new object[] { 0m, 0, 0m, 0m, 0m, 0m },
        new object[] { 1.5m, 0, 1m, 1m, 2m, 2m },
        new object[] { 1.5m, 1, 0m, 0m, 10m, 0m },
        new object[] { 1.5m, -1, 1.5m, 1.5m, 1.5m, 1.5m },

        new object[] { -1.5m, 0, -1m, -2m, -1m, -2m },
        new object[] { -1.5m, 1, 0m, -10m, 0m, 0m },
        new object[] { -1.5m, -1, -1.5m, -1.5m, -1.5m, -1.5m },

        new object[] { 1234m, 0, 1234m, 1234m, 1234m, 1234m },
        new object[] { 1234m, 1, 1230m, 1230m, 1240m, 1230m },
        new object[] { 1234m, 2, 1200m, 1200m, 1300m, 1200m },
        new object[] { 1234m, 3, 1000m, 1000m, 2000m, 1000m },
        new object[] { 1234m, 4, 0m, 0m, 10000m, 0m },

        new object[] { 98765e+10m, 0, 98765e+10m, 98765e+10m, 98765e+10m, 98765e+10m },
        new object[] { 98765e+10m, 11, 98760e+10m, 98760e+10m, 98770e+10m, 98770e+10m },
        new object[] { 98765e+10m, 13, 98000e+10m, 98000e+10m,  99000e+10m, 99000e+10m },
        new object[] { 98765e+10m, 20, 0m, 0m, 1e+20m, 0m },

        new object[] { 0.0234m, 0, 0m, 0m, 1m, 0m },
        new object[] { 0.0234m, -2, 0.02m, 0.02m, 0.03m, 0.02m },
        new object[] { -0.0234m, 10, 0m, -1e+10m, 0m, 0m },
        new object[] { -0.0234m, -2, -0.02m, -0.03m, -0.02m, -0.02m },
    };

    [TestCaseSource(nameof(TruncateTestData))]
    public void TestTruncate(decimal value, long exponent, decimal truncate, decimal floor, decimal ceiling, decimal round)
    {
        Assert.AreEqual((Incremental)truncate, Truncate(value, exponent));
        Assert.AreEqual((Incremental)truncate, Truncate(truncate, exponent));

        Assert.AreEqual((Incremental)floor, Floor(value, exponent));
        Assert.AreEqual((Incremental)floor, Floor(floor, exponent));

        Assert.AreEqual((Incremental)ceiling, Ceiling(value, exponent));
        Assert.AreEqual((Incremental)ceiling, Ceiling(ceiling, exponent));

        Assert.AreEqual((Incremental)round, Round(value, exponent));
        Assert.AreEqual((Incremental)round, Round(round, exponent));
    }
    //
    // [TestCaseSource(nameof(TruncateTestData))]
    // public void TestRound(Incremental value, long exponent, Incremental result)
    // {
    //     Assert.AreEqual(result, Truncate(value, exponent));
    //     Assert.AreEqual(result, Truncate(result, exponent));
    //
    //     Assert.AreEqual(-result, Truncate(-value, exponent));
    //     Assert.AreEqual(-result, Truncate(-result, exponent));
    // }
}
