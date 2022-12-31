# Incremental.Benchmarks

``` ini

BenchmarkDotNet=v0.13.3, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.101
  [Host]   : .NET 7.0.1 (7.0.122.56804), Arm64 RyuJIT AdvSIMD
  ShortRun : .NET 7.0.1 (7.0.122.56804), Arm64 RyuJIT AdvSIMD

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|         Method |           numberA |           numberB |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |------------------ |------------------ |----------:|----------:|----------:|------:|--------:|
| **IncrementalAdd** |                 **0** |                 **1** |  **4.020 ns** | **0.4828 ns** | **0.0265 ns** |  **1.00** |    **0.00** |
|     DecimalAdd |                 0 |                 1 |  4.082 ns | 1.6406 ns | 0.0899 ns |  1.02 |    0.02 |
|   BigDoubleAdd |                 0 |                 1 |  3.519 ns | 0.4376 ns | 0.0240 ns |  0.88 |    0.01 |
|                |                   |                   |           |           |           |       |         |
| **IncrementalAdd** |           **0.00015** |         **1.325E-09** |  **4.012 ns** | **0.2882 ns** | **0.0158 ns** |  **1.00** |    **0.00** |
|     DecimalAdd |           0.00015 |         1.325E-09 |  4.500 ns | 0.3138 ns | 0.0172 ns |  1.12 |    0.00 |
|   BigDoubleAdd |           0.00015 |         1.325E-09 | 15.688 ns | 0.7708 ns | 0.0422 ns |  3.91 |    0.02 |
|                |                   |                   |           |           |           |       |         |
| **IncrementalAdd** |              **0.02** |              **5050** |  **3.921 ns** | **0.2731 ns** | **0.0150 ns** |  **1.00** |    **0.00** |
|     DecimalAdd |              0.02 |              5050 |  4.831 ns | 0.6785 ns | 0.0372 ns |  1.23 |    0.01 |
|   BigDoubleAdd |              0.02 |              5050 | 16.532 ns | 0.1806 ns | 0.0099 ns |  4.22 |    0.01 |
|                |                   |                   |           |           |           |       |         |
| **IncrementalAdd** |       **31415920000** |         **245290000** |  **4.028 ns** | **0.1559 ns** | **0.0085 ns** |  **1.00** |    **0.00** |
|     DecimalAdd |       31415920000 |         245290000 |  3.969 ns | 0.3030 ns | 0.0166 ns |  0.99 |    0.01 |
|   BigDoubleAdd |       31415920000 |         245290000 | 15.973 ns | 0.1564 ns | 0.0086 ns |  3.97 |    0.01 |
|                |                   |                   |           |           |           |       |         |
| **IncrementalAdd** | **90.12308590830902** | **72.34995902389021** |  **7.412 ns** | **4.5212 ns** | **0.2478 ns** |  **1.00** |    **0.00** |
|     DecimalAdd | 90.12308590830902 | 72.34995902389021 |  4.747 ns | 2.2582 ns | 0.1238 ns |  0.64 |    0.01 |
|   BigDoubleAdd | 90.12308590830902 | 72.34995902389021 | 15.955 ns | 0.5000 ns | 0.0274 ns |  2.15 |    0.07 |
``` ini

BenchmarkDotNet=v0.13.3, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.101
  [Host]   : .NET 7.0.1 (7.0.122.56804), Arm64 RyuJIT AdvSIMD
  ShortRun : .NET 7.0.1 (7.0.122.56804), Arm64 RyuJIT AdvSIMD

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|         Method |           numberA |           numberB |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |------------------ |------------------ |----------:|----------:|----------:|------:|--------:|
| **IncrementalSub** |                 **0** |                 **1** |  **4.353 ns** | **0.6975 ns** | **0.0382 ns** |  **1.00** |    **0.00** |
|     DecimalSub |                 0 |                 1 |  4.041 ns | 0.7773 ns | 0.0426 ns |  0.93 |    0.02 |
|   BigDoubleSub |                 0 |                 1 |  4.343 ns | 0.4663 ns | 0.0256 ns |  1.00 |    0.01 |
|                |                   |                   |           |           |           |       |         |
| **IncrementalSub** |           **0.00015** |         **1.325E-09** |  **4.626 ns** | **0.0501 ns** | **0.0027 ns** |  **1.00** |    **0.00** |
|     DecimalSub |           0.00015 |         1.325E-09 |  4.337 ns | 0.1661 ns | 0.0091 ns |  0.94 |    0.00 |
|   BigDoubleSub |           0.00015 |         1.325E-09 | 17.328 ns | 1.2320 ns | 0.0675 ns |  3.75 |    0.01 |
|                |                   |                   |           |           |           |       |         |
| **IncrementalSub** |              **0.02** |              **5050** |  **4.459 ns** | **0.4011 ns** | **0.0220 ns** |  **1.00** |    **0.00** |
|     DecimalSub |              0.02 |              5050 |  4.826 ns | 0.3035 ns | 0.0166 ns |  1.08 |    0.01 |
|   BigDoubleSub |              0.02 |              5050 | 17.341 ns | 2.0171 ns | 0.1106 ns |  3.89 |    0.04 |
|                |                   |                   |           |           |           |       |         |
| **IncrementalSub** |       **31415920000** |         **245290000** |  **4.643 ns** | **0.3638 ns** | **0.0199 ns** |  **1.00** |    **0.00** |
|     DecimalSub |       31415920000 |         245290000 |  4.008 ns | 0.7087 ns | 0.0388 ns |  0.86 |    0.01 |
|   BigDoubleSub |       31415920000 |         245290000 | 17.455 ns | 0.8132 ns | 0.0446 ns |  3.76 |    0.03 |
|                |                   |                   |           |           |           |       |         |
| **IncrementalSub** | **90.12308590830902** | **72.34995902389021** |  **4.466 ns** | **1.6443 ns** | **0.0901 ns** |  **1.00** |    **0.00** |
|     DecimalSub | 90.12308590830902 | 72.34995902389021 |  4.650 ns | 0.1790 ns | 0.0098 ns |  1.04 |    0.02 |
|   BigDoubleSub | 90.12308590830902 | 72.34995902389021 | 17.414 ns | 1.8246 ns | 0.1000 ns |  3.90 |    0.07 |
``` ini

BenchmarkDotNet=v0.13.3, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.101
  [Host]   : .NET 7.0.1 (7.0.122.56804), Arm64 RyuJIT AdvSIMD
  ShortRun : .NET 7.0.1 (7.0.122.56804), Arm64 RyuJIT AdvSIMD

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|         Method |           numberA |           numberB |     Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |------------------ |------------------ |---------:|----------:|----------:|------:|--------:|
| **IncrementalMul** |                 **0** |                 **1** | **1.173 ns** | **0.1522 ns** | **0.0083 ns** |  **1.00** |    **0.00** |
|     DecimalMul |                 0 |                 1 | 4.074 ns | 0.4607 ns | 0.0253 ns |  3.47 |    0.04 |
|   BigDoubleMul |                 0 |                 1 | 2.470 ns | 0.1465 ns | 0.0080 ns |  2.11 |    0.02 |
|                |                   |                   |          |           |           |       |         |
| **IncrementalMul** |           **0.00015** |         **1.325E-09** | **2.526 ns** | **0.2706 ns** | **0.0148 ns** |  **1.00** |    **0.00** |
|     DecimalMul |           0.00015 |         1.325E-09 | 3.990 ns | 0.0853 ns | 0.0047 ns |  1.58 |    0.01 |
|   BigDoubleMul |           0.00015 |         1.325E-09 | 2.035 ns | 0.1042 ns | 0.0057 ns |  0.81 |    0.00 |
|                |                   |                   |          |           |           |       |         |
| **IncrementalMul** |              **0.02** |              **5050** | **2.894 ns** | **0.0703 ns** | **0.0039 ns** |  **1.00** |    **0.00** |
|     DecimalMul |              0.02 |              5050 | 4.051 ns | 0.6174 ns | 0.0338 ns |  1.40 |    0.01 |
|   BigDoubleMul |              0.02 |              5050 | 9.238 ns | 0.5901 ns | 0.0323 ns |  3.19 |    0.01 |
|                |                   |                   |          |           |           |       |         |
| **IncrementalMul** |       **31415920000** |         **245290000** | **2.534 ns** | **0.0858 ns** | **0.0047 ns** |  **1.00** |    **0.00** |
|     DecimalMul |       31415920000 |         245290000 | 5.584 ns | 0.5366 ns | 0.0294 ns |  2.20 |    0.01 |
|   BigDoubleMul |       31415920000 |         245290000 | 2.045 ns | 0.0318 ns | 0.0017 ns |  0.81 |    0.00 |
|                |                   |                   |          |           |           |       |         |
| **IncrementalMul** | **90.12308590830902** | **72.34995902389021** | **2.942 ns** | **0.1233 ns** | **0.0068 ns** |  **1.00** |    **0.00** |
|     DecimalMul | 90.12308590830902 | 72.34995902389021 | 6.220 ns | 1.5705 ns | 0.0861 ns |  2.11 |    0.03 |
|   BigDoubleMul | 90.12308590830902 | 72.34995902389021 | 9.295 ns | 1.6583 ns | 0.0909 ns |  3.16 |    0.03 |
``` ini

BenchmarkDotNet=v0.13.3, OS=macOS Monterey 12.3 (21E230) [Darwin 21.4.0]
Apple M1 Pro, 1 CPU, 10 logical and 10 physical cores
.NET SDK=7.0.101
  [Host]   : .NET 7.0.1 (7.0.122.56804), Arm64 RyuJIT AdvSIMD
  ShortRun : .NET 7.0.1 (7.0.122.56804), Arm64 RyuJIT AdvSIMD

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|         Method |           numberA |           numberB |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |------------------ |------------------ |----------:|----------:|----------:|------:|--------:|
| **IncrementalDiv** |                 **0** |                 **1** |  **1.299 ns** | **0.7998 ns** | **0.0438 ns** |  **1.00** |    **0.00** |
|     DecimalDiv |                 0 |                 1 |  5.138 ns | 0.2457 ns | 0.0135 ns |  3.96 |    0.12 |
|   BigDoubleDiv |                 0 |                 1 |  3.508 ns | 0.3155 ns | 0.0173 ns |  2.70 |    0.08 |
|                |                   |                   |           |           |           |       |         |
| **IncrementalDiv** |           **0.00015** |         **1.325E-09** | **13.099 ns** | **0.9768 ns** | **0.0535 ns** |  **1.00** |    **0.00** |
|     DecimalDiv |           0.00015 |         1.325E-09 | 23.576 ns | 0.7688 ns | 0.0421 ns |  1.80 |    0.01 |
|   BigDoubleDiv |           0.00015 |         1.325E-09 | 20.443 ns | 1.9786 ns | 0.1085 ns |  1.56 |    0.01 |
|                |                   |                   |           |           |           |       |         |
| **IncrementalDiv** |              **0.02** |              **5050** | **14.643 ns** | **0.1741 ns** | **0.0095 ns** |  **1.00** |    **0.00** |
|     DecimalDiv |              0.02 |              5050 | 18.283 ns | 0.9494 ns | 0.0520 ns |  1.25 |    0.00 |
|   BigDoubleDiv |              0.02 |              5050 | 10.159 ns | 0.9435 ns | 0.0517 ns |  0.69 |    0.00 |
|                |                   |                   |           |           |           |       |         |
| **IncrementalDiv** |       **31415920000** |         **245290000** | **13.067 ns** | **0.6480 ns** | **0.0355 ns** |  **1.00** |    **0.00** |
|     DecimalDiv |       31415920000 |         245290000 | 19.788 ns | 3.2056 ns | 0.1757 ns |  1.51 |    0.02 |
|   BigDoubleDiv |       31415920000 |         245290000 | 20.498 ns | 0.7021 ns | 0.0385 ns |  1.57 |    0.01 |
|                |                   |                   |           |           |           |       |         |
| **IncrementalDiv** | **90.12308590830902** | **72.34995902389021** | **22.004 ns** | **1.5544 ns** | **0.0852 ns** |  **1.00** |    **0.00** |
|     DecimalDiv | 90.12308590830902 | 72.34995902389021 | 44.807 ns | 4.4150 ns | 0.2420 ns |  2.04 |    0.02 |
|   BigDoubleDiv | 90.12308590830902 | 72.34995902389021 | 20.910 ns | 1.5380 ns | 0.0843 ns |  0.95 |    0.00 |


