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

