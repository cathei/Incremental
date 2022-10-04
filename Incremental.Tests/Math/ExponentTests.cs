// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System;
using System.Collections.Generic;
using NUnit.Framework;
using static Cathei.Mathematics.Incremental;

namespace Cathei.Mathematics.Tests;

public class ExponentTests
{
    public static IEnumerable<Incremental[]> ExpTestData = new List<Incremental[]>
    {
        new Incremental[] { 1, E },
        new Incremental[] { 10, 22026.4657948067165m },
        new Incremental[] { 12345, new(23_194_183_868_380_000, 5361) },
        new Incremental[] { 2, 7.389056098930650227m },
        new Incremental[] { E, 15.15426224147926418m },
        new Incremental[] { 0.08998m, 1.094152400438369651m },
        new Incremental[] { -7.25m, 0.00071017438884254906m },
        new Incremental[] { -92.14m, new(96_406_526_899_408_113, -41) },
        new Incremental[] { new(229394, -100), 1m },
    };

    [TestCaseSource(nameof(ExpTestData))]
    public void TestExp(Incremental value, Incremental expected)
    {
        value = Exp(value);

        long placeToCompare = expected.Exponent - Precision + 4;

        // approximate comparison
        value = Truncate(value, placeToCompare);
        expected = Truncate(expected, placeToCompare);

        Assert.AreEqual(expected, value);
    }

    [Test]
    public void TestRandomPow(
        [Random(0.00001, 10000.0, 5)] double value,
        [Random(-10.0, 10.0, 2)] double power)
    {
        Incremental result = Pow((Incremental)value, (Incremental)power);
        Incremental expected = (Incremental)Math.Pow(value, power);

        long placeToCompare = expected.Exponent - Precision + 4;

        Console.WriteLine($"Result: {result}\nExpected: {expected}");

        // approximate comparison
        result = Truncate(result, placeToCompare);
        expected = Truncate(expected, placeToCompare);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestPowException()
    {
        Assert.Throws<ArgumentException>(() => Pow(-1, 10));
        Assert.Throws<ArgumentException>(() => Pow(0, -1));
    }
}
