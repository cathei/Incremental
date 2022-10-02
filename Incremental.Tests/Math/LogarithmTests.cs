// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using NUnit.Framework;
using static Cathei.Mathematics.Incremental;

namespace Cathei.Mathematics.Tests;

public class LogarithmTests
{
    public static IEnumerable<Incremental[]> LogarithmTestData = new List<Incremental[]>
    {
        new Incremental[] { 1, 0 },
        new Incremental[] { 10, Ln10 },
        new Incremental[] { 12345, 9.421006401779279877m },
        new Incremental[] { 2, 0.69314718055994530m },
        new Incremental[] { E, 1 },
        new Incremental[] { 0.08998m, -2.40816785556911082m },
        new Incremental[] { new(669330, 100), 206.8311803025334027m },
        new Incremental[] { new(229394, -100), -254.7566744591613903m },
    };

    [TestCaseSource(nameof(LogarithmTestData))]
    public void TestNaturalLog(Incremental value, Incremental expected)
    {
        value = Log(value);

        long placeToCompare = expected.Exponent - Precision + 3;

        // approximate comparison
        value = Truncate(value, placeToCompare);
        expected = Truncate(expected, placeToCompare);

        Assert.AreEqual(expected, value);
    }

    [Test]
    public void TestRandomLog(
        [Random(0.00001, 10000.0, 5)] double value,
        [Random(0.00001, 10000.0, 2)] double @base)
    {
        Incremental result = Log((Incremental)value, (Incremental)@base);
        Incremental expected = (Incremental)Math.Log(value, @base);

        long placeToCompare = expected.Exponent - Precision + 3;

        Console.WriteLine($"Result: {result}\nExpected: {expected}");

        // approximate comparison
        result = Truncate(result, placeToCompare);
        expected = Truncate(expected, placeToCompare);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestLogException()
    {
        Assert.Throws<ArgumentException>(() => Log(-1));
        Assert.Throws<ArgumentException>(() => Log(123, 1));
        Assert.Throws<OverflowException>(() => Log(0));
    }
}
