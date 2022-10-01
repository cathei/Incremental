// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using static Cathei.Mathematics.Incremental;

namespace Cathei.Mathematics.Tests;

public class InternalMethodTests
{
    private static readonly BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Static;

    [Test]
    public void TestPowersOf10()
    {
        ulong[] powersOf10 = (ulong[])
            typeof(Incremental).GetField("PowersOf10", bindingFlags)?.GetValue(null);

        Assert.NotNull(powersOf10);

        ulong value = 1;

        for (int i = 0; i <= 19; ++i)
        {
            Assert.AreEqual(value, powersOf10[i]);
            value *= 10;
        }
    }

    [Test]
    public void TestMultiplyPow10()
    {
        var method = typeof(Incremental).GetMethod("MultiplyPow10", bindingFlags,
            types: new Type[] { typeof(ulong), typeof(int) });

        Assert.NotNull(method);

        for (int i = 0; i <= 19; ++i)
        {
            ulong value = (ulong)method.Invoke(null, new object[] { 1UL, i })!;

            Assert.AreEqual(
                (ulong)Math.Round(Math.Pow(10, i)), value);

            value = (ulong)method.Invoke(null, new object[] { value, -i })!;

            Assert.AreEqual(1, value);
        }
    }

    [Test]
    public void TestLog10([Random(0, ulong.MaxValue, 10)]ulong value)
    {
        var method = typeof(Incremental).GetMethod("Log10Int", bindingFlags);

        Assert.NotNull(method);

        int expected = 0;
        ulong copy = value;

        while (copy >= 10)
        {
            copy /= 10;
            expected++;
        }

        Assert.AreEqual(expected, method.Invoke(null, new object[] { value }));
    }
}
