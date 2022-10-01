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
    public void TestMax()
    {

    }
}
