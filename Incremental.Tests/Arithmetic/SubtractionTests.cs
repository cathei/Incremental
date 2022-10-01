// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using NUnit.Framework;
using static Cathei.Mathematics.Incremental;

namespace Cathei.Mathematics.Tests;

public class SubtractionTests
{
    public static IEnumerable<Incremental[]> SubtractionTestData = new List<Incremental[]>
    {
        new Incremental[] { 0, 0, 0 },
        new Incremental[] { 0, 1, -1 },
        new Incremental[] { 10, 1, 9 },
        new Incremental[] { 150, 92, 58 },
        new Incremental[] { 2020, 19_940_822L, -19_938_802L },
        new Incremental[] { 1_234_567_890L, 0, 1_234_567_890L },
        new Incremental[] { 9_876_547_890L, 10, 9_876_547_880L },
        new Incremental[] { 555_555_555, 44_444_444, 511_111_111 },
        new Incremental[] { 0.000_001m, 0.000_100m, -0.000_099m },
        new Incremental[] { 0.123_456m, 0.333m, -0.209_544m },
        new Incremental[] { 10.03m, 2.08m, 7.95m },
        new Incremental[] { 99.999m, 1.111m, 98.888m },
        new Incremental[] { new(Unit, 99), new(Unit, 0), new(Unit, 99) },
        new Incremental[] { new(10_000_000_000_000_001, 0), 0, new(10_000_000_000_000_001, 0) },

        new Incremental[]
        {
            new(90_000_000_000_000_001, 0),
            new(1, 0),
            new(90_000_000_000_000_000, 0),
        },

        new Incremental[]
        {
            new(90_000_000_000_000_001, 1),
            new(9, 0),
            new(90_000_000_000_000_001, 1),
        },

        // used in benchmark
        new Incremental[] { 3.141592e+10m, 2.45290e+8m, 3.117063e+10m },
        new Incremental[] { 0.00015m, 0.00000001325m, 0.00014998675m },
        new Incremental[] { 90.12308590830902345m, 72.3499590238902103m, 17.77312688441881315m },
        new Incremental[] { 0.02m, 5050, -5049.98m },
    };

    [TestCaseSource(nameof(SubtractionTestData))]
    public void TestSubtraction(Incremental a, Incremental b, Incremental result)
    {
        Assert.AreEqual(result, a - b);
        Assert.AreEqual(result, -b + a);
        Assert.AreEqual(result, Incremental.Subtract(a, b));
    }

    public static IEnumerable<Incremental[]> DecrementTestData = new List<Incremental[]>
    {
        new Incremental[] { 0, -1 },
        new Incremental[] { 1, 0 },
        new Incremental[] { 2, 1 },
        new Incremental[] { 1000, 999 },
        new Incremental[] { 1.001m, 0.001m },
        new Incremental[] { 0.999m, -0.001m },
    };

    [TestCaseSource(nameof(DecrementTestData))]
    public void TestDecrement(Incremental value, Incremental result)
    {
        Assert.AreEqual(result, --value);
    }
}