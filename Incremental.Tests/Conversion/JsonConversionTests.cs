// Incremental, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2022

using System.Collections.Generic;
using Newtonsoft.Json;
using NuGet.Frameworks;
using NUnit.Framework;
using static Cathei.Mathematics.Incremental;

namespace Cathei.Mathematics.Tests;

public class JsonConversionTests
{
    public static IEnumerable<object[]> ConvertJsonTestData = new List<object[]>
    {
        new object[] { Zero, "\"0\"" },
        new object[] { One, "\"1\"" },

        new object[] { (Incremental)0.7m, "\"0.7\"" },
        new object[] { -(Incremental)0.7m, "\"-0.7\"" },

        new object[] { (Incremental)0.00027m, "\"0.00027\"" },
        new object[] { -(Incremental)0.00027m, "\"-0.00027\"" },

        new object[] { (Incremental)0.000027m, "\"2.7e-5\"" },
        new object[] { -(Incremental)0.000027m, "\"-2.7e-5\"" },

        new object[] { (Incremental)12.45m, "\"12.45\"" },
        new object[] { -(Incremental)12.45m, "\"-12.45\"" },

        new object[] { (Incremental)4978500m, "\"4978500\"" },
        new object[] { -(Incremental)4978500m, "\"-4978500\"" },

        new object[] { new Incremental(Unit, Precision), "\"10000000000000000\"" },
        new object[] { new Incremental(Unit, Precision + 1), "\"1e+17\"" },

        new object[] { new Incremental(34_028_234_663_852_886L, 38), "\"3.4028234663852886e+38\""  },
        new object[] { new Incremental(-34_028_234_663_852_886L, 38), "\"-3.4028234663852886e+38\""  },

        new object[] { new Incremental(34_028_234_663_852_886L, -38), "\"3.4028234663852886e-38\""  },
        new object[] { new Incremental(-34_028_234_663_852_886L, -38), "\"-3.4028234663852886e-38\""  },

        new object[] { new Incremental(54_321_000_000_000_000L, 799), "\"5.4321e+799\""  },
        new object[] { new Incremental(-54_321_000_000_000_000L, 799), "\"-5.4321e+799\""  },

        new object[] { new Incremental(54_321_000_000_000_000L, -799), "\"5.4321e-799\""  },
        new object[] { new Incremental(-54_321_000_000_000_000L, -799), "\"-5.4321e-799\""  },
    };

    [TestCaseSource(nameof(ConvertJsonTestData))]
    public void TestConvertJson(Incremental value, string json)
    {
        Assert.AreEqual(json, JsonConvert.SerializeObject(value));
        Assert.AreEqual(value, JsonConvert.DeserializeObject<Incremental>(json));
    }
}
