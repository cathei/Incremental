// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using NUnit.Framework;
using static Cathei.Mathematics.Incremental;

namespace Cathei.Mathematics.Tests;

public class AdditionTests
{
    public static IEnumerable<Incremental[]> AdditionTestData = new List<Incremental[]>
    {
        new Incremental[] { 0, 0, 0 },
        new Incremental[] { 0, 1, 1 },
        new Incremental[] { 10, 1, 11 },
        new Incremental[] { 150, 92, 242 },
        new Incremental[] { 2020, 19_940_822L, 19_942_842L },
        new Incremental[] { 1_234_567_890L, 0, 1_234_567_890L },
        new Incremental[] { 9_876_547_890L, 10, 9_876_547_900L },
        new Incremental[] { 555_555_555, 44_444_444, 599_999_999 },
        new Incremental[] { 0.000_001m, 0.000_1m, 0.000_101m },
        new Incremental[] { 0.123_456m, 0.333m, 0.456_456m },
        new Incremental[] { 10.03m, 2.08m, 12.11m },
        new Incremental[] { 99.999m, 1.111m, 101.110m },
        new Incremental[] { new(Unit, 99), new(Unit, 0), new(Unit, 99) },
        new Incremental[] { new(10_000_000_000_000_001, 0), 0, new(10_000_000_000_000_001, 0) },
    };

    [TestCaseSource(nameof(AdditionTestData))]
    public void TestAddition(Incremental a, Incremental b, Incremental result)
    {
        Assert.AreEqual(result, a + b);
        Assert.AreEqual(result, b + a);
        Assert.AreEqual(result, Incremental.Add(a, b));
    }

    public static IEnumerable<Incremental[]> IncrementTestData = new List<Incremental[]>
    {
        new Incremental[] { -1, 0 },
        new Incremental[] { 0, 1 },
        new Incremental[] { 1, 2 },
        new Incremental[] { 1000, 1001 },
        new Incremental[] { 0.001m, 1.001m },
        new Incremental[] { -0.001m, 0.999m },
    };

    [TestCaseSource(nameof(IncrementTestData))]
    public void TestIncrement(Incremental value, Incremental result)
    {
        Assert.AreEqual(result, ++value);
    }
}