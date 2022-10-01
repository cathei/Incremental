// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using NUnit.Framework;
using static Cathei.Mathematics.Incremental;

namespace Cathei.Mathematics.Tests;

public class TruncateTests
{
    public static IEnumerable<object[]> TruncateTestData = new List<object[]>
    {
        new object[] { One, 0, One },
        new object[] { Zero, 0, Zero },
        new object[] { (Incremental)1.5m, 0, One },
        new object[] { (Incremental)1.5m, 1, Zero },
        new object[] { (Incremental)1.5m, -1, (Incremental)1.5m },

        new object[] { (Incremental)1234, 0, (Incremental)1234 },
        new object[] { (Incremental)1234, 1, (Incremental)1230 },
        new object[] { (Incremental)1234, 2, (Incremental)1200 },
        new object[] { (Incremental)1234, 3, (Incremental)1000 },
        new object[] { (Incremental)1234, 4, Zero },

        new object[] { new Incremental(98765, Precision + 100), 0, new Incremental(98765, Precision + 100) },
        new object[] { new Incremental(98765, Precision + 100), 101, new Incremental(98760, Precision + 100) },
        new object[] { new Incremental(98765, Precision + 100), 103, new Incremental(98000, Precision + 100) },
        new object[] { new Incremental(98765, Precision + 100), 999, Zero },

        new object[] { (Incremental)0.0234m, 0, Zero },
        new object[] { (Incremental)0.0234m, 999, Zero },
        new object[] { (Incremental)0.0234m, -2, (Incremental)0.02m },
        new object[] { (Incremental)0.0234m, -999, (Incremental)0.0234m },
    };

    [TestCaseSource(nameof(TruncateTestData))]
    public void TestTruncate(Incremental value, long exponent, Incremental result)
    {
        Assert.AreEqual(result, Truncate(value, exponent));
        Assert.AreEqual(result, Truncate(result, exponent));

        Assert.AreEqual(-result, Truncate(-value, exponent));
        Assert.AreEqual(-result, Truncate(-result, exponent));
    }
}
