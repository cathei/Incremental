// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using NUnit.Framework;
using static Cathei.Mathematics.Incremental;

namespace Cathei.Mathematics.Tests;

public class OperationTests
{
    [Test]
    public void TestAbs([Random(-1000000, 1000000, 20)] long value)
    {
        Incremental inc = value;
        Assert.AreEqual(Math.Abs(value), (long)Abs(inc));
        Assert.AreEqual(Math.Abs(value), (long)Abs(-inc));
    }

    [Test]
    public void TestPow10()
    {
        Assert.AreEqual(0.001m, (decimal)Pow10(-3));
        Assert.AreEqual(0.1m, (decimal)Pow10(-1));
        Assert.AreEqual(1m, (decimal)Pow10(0));
        Assert.AreEqual(1000m, (decimal)Pow10(3));
        Assert.AreEqual(100000m, (decimal)Pow10(5));
    }
}
