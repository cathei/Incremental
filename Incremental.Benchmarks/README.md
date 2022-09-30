# Incremental.Benchmarks

Current snapshot of the benchmark to compare later versions

|         Method |        Mean |     Error |    StdDev |      Median |
|--------------- |------------:|----------:|----------:|------------:|
|      DoubleAdd |   0.0062 ns | 0.0099 ns | 0.0087 ns |   0.0011 ns |
|     DecimalAdd |   8.0967 ns | 0.1036 ns | 0.0969 ns |   8.0886 ns |
| IncrementalAdd |   7.1305 ns | 0.1701 ns | 0.1958 ns |   7.1016 ns |
|   BigDoubleAdd |  18.5079 ns | 0.2921 ns | 0.2589 ns |  18.4859 ns |
|      DoubleSub |   0.0091 ns | 0.0146 ns | 0.0136 ns |   0.0000 ns |
|     DecimalSub |   8.3307 ns | 0.1354 ns | 0.1267 ns |   8.2854 ns |
| IncrementalSub |   8.2460 ns | 0.0861 ns | 0.0719 ns |   8.2661 ns |
|   BigDoubleSub |  19.0314 ns | 0.3414 ns | 0.2851 ns |  19.1082 ns |
|      DoubleMul |   0.0061 ns | 0.0086 ns | 0.0072 ns |   0.0037 ns |
|     DecimalMul |  18.8329 ns | 0.2410 ns | 0.2136 ns |  18.8710 ns |
| IncrementalMul |  46.8355 ns | 0.6402 ns | 0.4998 ns |  46.8660 ns |
|   BigDoubleMul |  12.7399 ns | 0.2102 ns | 0.1755 ns |  12.7384 ns |
|      DoubleDiv |   0.0126 ns | 0.0128 ns | 0.0120 ns |   0.0101 ns |
|     DecimalDiv |  58.6312 ns | 0.5310 ns | 0.4967 ns |  58.4040 ns |
| IncrementalDiv | 109.7989 ns | 0.5041 ns | 0.4209 ns | 109.7557 ns |
|   BigDoubleDiv |  30.5874 ns | 0.3522 ns | 0.3122 ns |  30.6722 ns |

|         Method |       Mean |      Error |    StdDev |     Median |
|--------------- |-----------:|-----------:|----------:|-----------:|
|      DoubleAdd |  0.0052 ns |  0.1633 ns | 0.0090 ns |  0.0000 ns |
|     DecimalAdd |  7.4990 ns |  1.2137 ns | 0.0665 ns |  7.5231 ns |
| IncrementalAdd | 12.3195 ns |  0.2404 ns | 0.0132 ns | 12.3239 ns |
|   BigDoubleAdd | 18.4258 ns |  2.1335 ns | 0.1169 ns | 18.4114 ns |
|      DoubleSub |  0.0032 ns |  0.0649 ns | 0.0036 ns |  0.0025 ns |
|     DecimalSub |  7.4831 ns |  0.7438 ns | 0.0408 ns |  7.5064 ns |
| IncrementalSub |  6.8720 ns |  0.8898 ns | 0.0488 ns |  6.8655 ns |
|   BigDoubleSub | 18.4282 ns |  5.0289 ns | 0.2756 ns | 18.4022 ns |
|      DoubleMul |  0.0124 ns |  0.2640 ns | 0.0145 ns |  0.0090 ns |
|     DecimalMul | 11.9808 ns |  1.1787 ns | 0.0646 ns | 11.9676 ns |
| IncrementalMul | 46.2155 ns |  6.3903 ns | 0.3503 ns | 46.3346 ns |
|   BigDoubleMul | 12.3167 ns |  0.8004 ns | 0.0439 ns | 12.3309 ns |
|      DoubleDiv |  0.0193 ns |  0.3021 ns | 0.0166 ns |  0.0269 ns |
|     DecimalDiv | 58.0784 ns |  3.1519 ns | 0.1728 ns | 58.0736 ns |
| IncrementalDiv | 87.9172 ns | 54.1510 ns | 2.9682 ns | 86.3076 ns |
|   BigDoubleDiv | 30.7582 ns |  9.8533 ns | 0.5401 ns | 30.5377 ns |

|         Method |       Mean |     Error |    StdDev |     Median |
|--------------- |-----------:|----------:|----------:|-----------:|
|      DoubleAdd |  0.0089 ns | 0.0092 ns | 0.0086 ns |  0.0070 ns |
|     DecimalAdd |  7.9391 ns | 0.1304 ns | 0.1156 ns |  7.9560 ns |
| IncrementalAdd |  6.9892 ns | 0.0874 ns | 0.0775 ns |  7.0097 ns |
|   BigDoubleAdd | 18.4090 ns | 0.3410 ns | 0.3190 ns | 18.3624 ns |
|      DoubleSub |  0.0093 ns | 0.0086 ns | 0.0076 ns |  0.0094 ns |
|     DecimalSub |  7.5029 ns | 0.0517 ns | 0.0483 ns |  7.4954 ns |
| IncrementalSub |  8.2930 ns | 0.1021 ns | 0.0955 ns |  8.2874 ns |
|   BigDoubleSub | 18.5657 ns | 0.2745 ns | 0.2433 ns | 18.5563 ns |
|      DoubleMul |  0.0065 ns | 0.0082 ns | 0.0073 ns |  0.0029 ns |
|     DecimalMul | 12.0125 ns | 0.0544 ns | 0.0509 ns | 12.0145 ns |
| IncrementalMul | 40.2848 ns | 0.5998 ns | 0.5317 ns | 40.3547 ns |
|   BigDoubleMul | 12.1954 ns | 0.1531 ns | 0.1432 ns | 12.1759 ns |
|      DoubleDiv |  0.0137 ns | 0.0168 ns | 0.0140 ns |  0.0072 ns |
|     DecimalDiv | 61.6540 ns | 0.6998 ns | 0.6204 ns | 61.8346 ns |
| IncrementalDiv | 96.8273 ns | 0.4603 ns | 0.4081 ns | 96.8391 ns |
|   BigDoubleDiv | 30.8499 ns | 0.4897 ns | 0.4341 ns | 30.8442 ns |

