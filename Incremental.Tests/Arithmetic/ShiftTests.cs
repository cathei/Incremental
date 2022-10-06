// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using NUnit.Framework;

namespace Cathei.Mathematics.Tests;

public class ShiftTests
{
    public static IEnumerable<object[]> ShiftTestData = new List<object[]>
    {
        new object[] { 0m, 0, 0m },
        new object[] { 1m, 1, 0.1m },
        new object[] { 123m, -1, 123m },
        new object[] { 7000m, 4, 0.7m },
        new object[] { 8780200m, 3, 8780.2m },
        new object[] { 0.000025m, 8, 2.5e-13m },
        new object[] { 1.95232415151m, 0, 1.95232415151m }
    };

    [TestCaseSource(nameof(ShiftTestData))]
    public void TestShift(decimal value, int shift, decimal result)
    {
        Assert.AreEqual((Incremental)result, (Incremental)value >> shift);
        Assert.AreEqual((Incremental)value, (Incremental)result << shift);
    }
}