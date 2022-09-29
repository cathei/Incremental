# Incremental.Benchmarks

Current snapshot of the benchmark to compare later versions

|         Method |       Mean |     Error |    StdDev |     Median |
|--------------- |-----------:|----------:|----------:|-----------:|
|      DoubleAdd |  0.0004 ns | 0.0010 ns | 0.0010 ns |  0.0000 ns |
|     DecimalAdd |  2.9518 ns | 0.0229 ns | 0.0214 ns |  2.9538 ns |
| IncrementalAdd |  3.7558 ns | 0.0176 ns | 0.0164 ns |  3.7569 ns |
|      DoubleSub |  0.0126 ns | 0.0005 ns | 0.0004 ns |  0.0127 ns |
|     DecimalSub |  2.8384 ns | 0.0136 ns | 0.0127 ns |  2.8334 ns |
| IncrementalSub | 12.3756 ns | 0.0780 ns | 0.0729 ns | 12.3612 ns |
|      DoubleMul |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |
|     DecimalMul |  5.8590 ns | 0.0285 ns | 0.0267 ns |  5.8565 ns |
| IncrementalMul | 32.6915 ns | 0.0595 ns | 0.0497 ns | 32.6707 ns |
|      DoubleDiv |  0.0141 ns | 0.0029 ns | 0.0024 ns |  0.0147 ns |
|     DecimalDiv | 47.1762 ns | 0.1000 ns | 0.0936 ns | 47.2045 ns |
| IncrementalDiv | 64.1715 ns | 0.2836 ns | 0.2653 ns | 64.1845 ns |


|         Method |       Mean |     Error |    StdDev |     Median |
|--------------- |-----------:|----------:|----------:|-----------:|
|      DoubleAdd |  0.0198 ns | 0.0016 ns | 0.0014 ns |  0.0195 ns |
|     DecimalAdd |  2.8624 ns | 0.0101 ns | 0.0095 ns |  2.8657 ns |
| IncrementalAdd | 16.5155 ns | 0.0747 ns | 0.0699 ns | 16.5276 ns |
|      DoubleSub |  0.0008 ns | 0.0010 ns | 0.0010 ns |  0.0000 ns |
|     DecimalSub |  4.4117 ns | 0.0150 ns | 0.0133 ns |  4.4124 ns |
| IncrementalSub |  6.3578 ns | 0.0391 ns | 0.0366 ns |  6.3539 ns |
|      DoubleMul |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |
|     DecimalMul | 12.3572 ns | 0.0436 ns | 0.0386 ns | 12.3642 ns |
| IncrementalMul | 36.4546 ns | 0.1452 ns | 0.1287 ns | 36.4499 ns |
|      DoubleDiv |  0.0170 ns | 0.0065 ns | 0.0057 ns |  0.0164 ns |
|     DecimalDiv | 46.0368 ns | 0.3010 ns | 0.2815 ns | 46.0332 ns |
| IncrementalDiv | 68.8541 ns | 0.1884 ns | 0.1762 ns | 68.8106 ns |

Ran on Windows:

|         Method |        Mean |     Error |    StdDev |      Median |
|--------------- |------------:|----------:|----------:|------------:|
|      DoubleAdd |   0.0569 ns | 0.0322 ns | 0.0371 ns |   0.0504 ns |
|     DecimalAdd |   7.5306 ns | 0.0523 ns | 0.0464 ns |   7.5322 ns |
| IncrementalAdd |  20.6842 ns | 0.4398 ns | 0.8473 ns |  20.4076 ns |
|      DoubleSub |   0.0084 ns | 0.0203 ns | 0.0170 ns |   0.0000 ns |
|     DecimalSub |   8.0097 ns | 0.1892 ns | 0.1858 ns |   7.9864 ns |
| IncrementalSub |  31.8944 ns | 0.6336 ns | 0.7782 ns |  31.6002 ns |
|      DoubleMul |   0.0024 ns | 0.0063 ns | 0.0074 ns |   0.0000 ns |
|     DecimalMul |  17.2764 ns | 0.3738 ns | 0.7637 ns |  17.1612 ns |
| IncrementalMul |  60.3189 ns | 1.2352 ns | 2.9356 ns |  59.4239 ns |
|      DoubleDiv |   0.0299 ns | 0.0243 ns | 0.0462 ns |   0.0000 ns |
|     DecimalDiv |  73.9424 ns | 0.8324 ns | 0.6951 ns |  73.6304 ns |
| IncrementalDiv | 108.0497 ns | 1.8297 ns | 1.7115 ns | 108.2805 ns |

|         Method |        Mean |     Error |    StdDev |      Median |
|--------------- |------------:|----------:|----------:|------------:|
|      DoubleAdd |   0.0234 ns | 0.0264 ns | 0.0234 ns |   0.0150 ns |
|     DecimalAdd |   7.4399 ns | 0.0525 ns | 0.0491 ns |   7.4285 ns |
| IncrementalAdd |  18.0478 ns | 0.2475 ns | 0.2194 ns |  18.0290 ns |
|      DoubleSub |   0.0284 ns | 0.0236 ns | 0.0209 ns |   0.0342 ns |
|     DecimalSub |   7.5624 ns | 0.0437 ns | 0.0387 ns |   7.5541 ns |
| IncrementalSub |   8.9079 ns | 0.1957 ns | 0.1831 ns |   8.9106 ns |
|      DoubleMul |   0.0171 ns | 0.0178 ns | 0.0158 ns |   0.0130 ns |
|     DecimalMul |  15.8707 ns | 0.2470 ns | 0.2190 ns |  15.9018 ns |
| IncrementalMul |  43.6002 ns | 0.3880 ns | 0.3629 ns |  43.7618 ns |
|      DoubleDiv |   0.0118 ns | 0.0073 ns | 0.0068 ns |   0.0099 ns |
|     DecimalDiv |  61.8014 ns | 0.7942 ns | 0.7429 ns |  61.8070 ns |
| IncrementalDiv | 104.2953 ns | 0.7916 ns | 0.7404 ns | 104.2508 ns |

|         Method |        Mean |       Error |    StdDev |      Median |
|--------------- |------------:|------------:|----------:|------------:|
|      DoubleAdd |   0.0314 ns |   0.2456 ns | 0.0135 ns |   0.0385 ns |
|     DecimalAdd |   8.2372 ns |   0.3867 ns | 0.0212 ns |   8.2355 ns |
| IncrementalAdd |   5.4503 ns |   3.8663 ns | 0.2119 ns |   5.3833 ns |
|   BigDoubleAdd |  18.3361 ns |   7.5557 ns | 0.4142 ns |  18.2916 ns |
|      DoubleSub |   0.0086 ns |   0.1925 ns | 0.0106 ns |   0.0055 ns |
|     DecimalSub |   7.5102 ns |   1.3525 ns | 0.0741 ns |   7.5008 ns |
| IncrementalSub |  22.6846 ns |   2.5584 ns | 0.1402 ns |  22.6960 ns |
|   BigDoubleSub |  18.4848 ns |   4.7094 ns | 0.2581 ns |  18.5246 ns |
|      DoubleMul |   0.0109 ns |   0.1803 ns | 0.0099 ns |   0.0136 ns |
|     DecimalMul |  18.6501 ns |   2.0182 ns | 0.1106 ns |  18.7110 ns |
| IncrementalMul |  55.2706 ns |   4.2470 ns | 0.2328 ns |  55.1677 ns |
|   BigDoubleMul |  12.0113 ns |   2.0516 ns | 0.1125 ns |  12.0596 ns |
|      DoubleDiv |   0.0390 ns |   0.4242 ns | 0.0233 ns |   0.0286 ns |
|     DecimalDiv |  64.6322 ns |  40.6017 ns | 2.2255 ns |  64.4755 ns |
| IncrementalDiv | 131.0785 ns | 133.7155 ns | 7.3294 ns | 128.0690 ns |
|   BigDoubleDiv |  32.9992 ns |  21.2933 ns | 1.1672 ns |  32.5957 ns |



