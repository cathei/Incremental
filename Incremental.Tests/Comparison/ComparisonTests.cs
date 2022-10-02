// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Cathei.Mathematics.Tests;

public class ComparisonTests
{
    public static IEnumerable<Incremental[]> ComparisonTestData = new List<Incremental[]>
    {
        new Incremental[] { 0, 1 },
        new Incremental[] { -1, 0 },
        new Incremental[] { -1, 1 },
        new Incremental[] { 0, 10 },
        new Incremental[] { 1, 10 },
        new Incremental[] { 15, 16 },
        new Incremental[] { 0.08m, 10 },
        new Incremental[] { 0.008m, 0.1m },
        new Incremental[] { 0.06m, 0.07m },
        new Incremental[] { -10, -1 },
        new Incremental[] { -90, 100 },
        new Incremental[] { -0.01m, 0 },
        new Incremental[] { -0.04m, 100 },
        new Incremental[] { -0.4m, -0.04m },
        new Incremental[] { -0.07m, -0.03m },
        new Incremental[] { -0.8m, 0 },
        new Incremental[] { new(Incremental.Unit, -9), new(Incremental.Unit, -8) },
        new Incremental[] { new(Incremental.Unit, 9), new(Incremental.Unit + 1, 9) },
    };

    [TestCaseSource(nameof(ComparisonTestData))]
    public void TestComparison(Incremental a, Incremental b)
    {
        Assert.True(a < b);
        Assert.True(a <= b);

        Assert.False(a > b);
        Assert.False(a >= b);

        Assert.False(a == b);
        Assert.True(a != b);

        Assert.AreEqual(-1, a.CompareTo(b));
        Assert.AreEqual(1, b.CompareTo(a));

        Assert.AreEqual(a, Incremental.Min(a, b));
        Assert.AreEqual(a, Incremental.Min(b, a));

        Assert.AreEqual(b, Incremental.Max(a, b));
        Assert.AreEqual(b, Incremental.Max(b, a));
    }

    public static IEnumerable<Incremental[][]> SortingTestData = new List<Incremental[][]>
    {
        new[]
        {
            new Incremental[] { 10, 838, 9, 1, -9, 2, 0.1m, 22, 1.1m, -9, -0.023m, 10000002 },
            new Incremental[] { -9, -9, -0.023m, 0.1m, 1, 1.1m, 2, 9, 10, 22, 838, 10000002 }
        }
    };


    [TestCaseSource(nameof(SortingTestData))]
    public void TestSorting(Incremental[] original, Incremental[] expected)
    {
        Array.Sort(original);

        CollectionAssert.AreEqual(expected, original);
    }
}
