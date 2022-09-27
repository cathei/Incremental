// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using NUnit.Framework;

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
        // new Incremental[] { new(1, 99), Incremental.One, new(1, 99) },
    };

    [TestCaseSource(nameof(SubtractionTestData))]
    public void TestSubtraction(Incremental a, Incremental b, Incremental result)
    {
        Assert.AreEqual(result, a - b);
        Assert.AreEqual(result, -b + a);
        Assert.AreEqual(result, Incremental.Subtract(a, b));
    }
}