// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using NUnit.Framework;
using static Cathei.Mathematics.Incremental;

namespace Cathei.Mathematics.Tests;

public class DecimalConversionTests
{
    public static IEnumerable<object[]> ConvertIntTestData = new List<object[]>
    {
        new object[] { Incremental.Zero, 0 },
        new object[] { Incremental.One, 1 },
        new object[] { new Incremental(2147483647, Precision), int.MaxValue },
        new object[] { new Incremental(-2147483648, Precision), int.MinValue },
    };

    [TestCaseSource(nameof(ConvertIntTestData))]
    public void TestConvertInt(Incremental a, int b)
    {
        Assert.AreEqual(b, (int)a);
        Assert.AreEqual(a, (Incremental)b);
    }

    public static IEnumerable<object[]> ConvertLongTestData = new List<object[]>
    {
        new object[] { Incremental.Zero, 0L },
        new object[] { Incremental.One, 1L },

        new object[] { new Incremental(2147483647L, Precision), (long)int.MaxValue },
        new object[] { new Incremental(-2147483648L, Precision), (long)int.MinValue },

        // last two digit will not be preserved.
        // we also cannot take Abs for long type min value, it will cause OverflowException
        new object[] { new Incremental(92_233_720_368_547_758L, Precision + 2), long.MaxValue - 7L },
        new object[] { new Incremental(-92_233_720_368_547_758L, Precision + 2), long.MinValue + 8L },
    };

    [TestCaseSource(nameof(ConvertLongTestData))]
    public void TestConvertLong(Incremental a, long b)
    {
        Assert.AreEqual(b, (long)a);
        Assert.AreEqual(a, (Incremental)b);
    }

    public static IEnumerable<object[]> ConvertDecimalTestData = new List<object[]>
    {
        new object[] { Incremental.Zero, 0m },
        new object[] { Incremental.One, 1m },
        new object[] { new Incremental(01_000_000_000_000_000, 0), 0.1m },
        new object[] { new Incremental(00_000_000_000_000_001, 0), 0.0_000_000_000_000_001m },

        new object[] { new Incremental(2147483647L, Precision), (decimal)int.MaxValue },
        new object[] { new Incremental(-2147483648L, Precision), (decimal)int.MinValue },

        // last digit will not be preserved.
        // decimal can be used for bigger range than long
        new object[] { new Incremental(92_233_720_368_547_759L, Precision + 2), long.MaxValue + 93m },
        new object[] { new Incremental(-92_233_720_368_547_759L, Precision + 2), long.MinValue - 92m },

        // approx. min max value
        new object[] { new Incremental(79_228_162_514_264_337L, 28), 79228162514264337000000000000m },
        new object[] { new Incremental(-79_228_162_514_264_337L, 28), -79228162514264337000000000000m },
    };

    [TestCaseSource(nameof(ConvertDecimalTestData))]
    public void TestConvertDecimal(Incremental a, decimal b)
    {
        Assert.AreEqual(b, (decimal)a);
        Assert.AreEqual(a, (Incremental)b);
    }
}
