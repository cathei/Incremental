// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using NUnit.Framework;

namespace Cathei.Mathematics.Tests;

public class EqualityTests
{
    public static IEnumerable<Incremental[]> EqualityTestData = new List<Incremental[]>
    {
        new Incremental[] { 0 },
        new Incremental[] { 1 },
        new Incremental[] { 10 },
        new Incremental[] { 0.08m },
        new Incremental[] { 0.000001m },
        new Incremental[] { 453535344223 },
        new Incremental[] { 884200402000100000 },
        new Incremental[] { -727484220020 },
        new Incremental[] { -4252.1555m },
    };

    [TestCaseSource(nameof(EqualityTestData))]
    public void TestEquality(Incremental org)
    {
        Incremental copy = new(org.Mantissa, org.Exponent);

        Assert.False(org < copy);
        Assert.True(org <= copy);

        Assert.False(org > copy);
        Assert.True(org >= copy);

        Assert.False(org != copy);
        Assert.True(org == copy);

        Assert.True(org.Equals(copy));
        Assert.True(org.Equals((object)copy));
    }
}
