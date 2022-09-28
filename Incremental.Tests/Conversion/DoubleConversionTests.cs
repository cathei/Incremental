// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using NuGet.Frameworks;
using NUnit.Framework;
using static Cathei.Mathematics.Incremental;

namespace Cathei.Mathematics.Tests;

public class DoubleConversionTests
{
    public static IEnumerable<object[]> ConvertFloatTestData = new List<object[]>
    {
        new object[] { Incremental.Zero, 0f, 0.0001f },
        new object[] { Incremental.One, 1f, 0.0001f },

        new object[] { new Incremental(7, Precision - 1), 0.7f, 0.0001f },
        new object[] { new Incremental(-7, Precision - 1), -0.7f, 0.0001f },

        new object[] { new Incremental(1245, Precision - 2), 12.45f, 0.0001f },
        new object[] { new Incremental(-1245, Precision - 2), -12.45f, 0.0001f },

        new object[] { new Incremental(49785, Precision + 2), 4978500f, 10f },
        new object[] { new Incremental(-49785, Precision + 2), -4978500f, 10f },

    };

    [TestCaseSource(nameof(ConvertFloatTestData))]
    public void TestConvertFloat(Incremental a, float b, float delta)
    {
        Assert.AreEqual(b, (float)a, delta);
        Assert.True(a > b - delta, "Value: {0} was not bigger than {1} - {2}", a, b, delta);
        Assert.True(a < b + delta, "Value: {0} was not smaller than {1} + {2}", a, b, delta);
    }

    public static IEnumerable<object[]> ConvertDoubleTestData = new List<object[]>
    {
        new object[] { Incremental.Zero, 0.0, 0.000001 },
        new object[] { Incremental.One, 1.0, 0.000001 },

        new object[] { new Incremental(7, Precision - 1), 0.7, 0.0000001 },
        new object[] { new Incremental(-7, Precision - 1), -0.7, 0.0000001 },

        new object[] { new Incremental(1245, Precision - 2), 12.45, 0.0000001 },
        new object[] { new Incremental(-1245, Precision - 2), -12.45, 0.0000001 },

        new object[] { new Incremental(49785, Precision + 2), 4978500.0, 0.1 },
        new object[] { new Incremental(-49785, Precision + 2), -4978500.0, 0.1 },

        new object[] { new Incremental(34_028_234_663_852_886L, 38), (double)float.MaxValue, 1e+32},
        new object[] { new Incremental(-34_028_234_663_852_886L, 38), (double)float.MinValue, 1e+32 },

        new object[] { new Incremental(14_000_000_000_000_000L, -45), (double)float.Epsilon, 1e-46 },
        new object[] { new Incremental(-14_000_000_000_000_000L, -45), (double)-float.Epsilon, 1e-46 },
    };

    [TestCaseSource(nameof(ConvertDoubleTestData))]
    public void TestConvertDouble(Incremental a, double b, double delta)
    {
        Assert.AreEqual(b, (double)a, delta);
        Assert.True(a > b - delta, "Value: {0} was not bigger than {1} - {2}", a, b, delta);
        Assert.True(a < b + delta, "Value: {0} was not smaller than {1} + {2}", a, b, delta);
    }

}
