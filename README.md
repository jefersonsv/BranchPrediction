# What is Branch Prediction?

In computer architecture, is a digital circuit that tries to guess which way a branch (e.g. an if/else structure) will go before this is known definitively.
Branch prediction play a critical role in achieving high effective perfomance in modern microprocessor.

* [Reference](https://en.wikipedia.org/wiki/Branch_predictor) - Branch predictor - Wikipedia

# Explaination

The class ``BranchPredictionRunner`` has two methods to compare benchmark. The constructor of the class has two instance of new array with lenght (UInt16.MaxValue). The first array has random integers unordered and the second has the same integers ordered ascending.

1. WithSort() - This method do a loop into the ordered array and sum the values if the number is less than the middle (UInt16.MaxValue / 2)
2. WithoutSort() - This method do a loop into the unordered array and sum the values if the number is less than the middle (UInt16.MaxValue / 2)

# Results

Looking the results you may perceive that the method WithSort was executed fatest than WithoutSort, 117 Microsecond vs 411 Microsecond.
This occours because when the microprocessor try to guess which way the result of conditional (if/else) will occours, the index of win is bigger.

> On method WithSort the number of wrong conditional is lower because after the first false conditional always others will be false also.

# Benchmark
```
BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i5-3210M CPU 2.50GHz (Ivy Bridge), 1 CPU, 4 logical and 2 physical cores
Frequency=2435877 Hz, Resolution=410.5298 ns, Timer=TSC
  [Host] : .NET Framework 4.7.1 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3120.0
  Clr    : .NET Framework 4.7.1 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3120.0

Job=Clr  Runtime=Clr  

      Method |     Mean |    Error |    StdDev |   Median | Rank |
------------ |---------:|---------:|----------:|---------:|-----:|
 WithoutSort | 411.1 us | 8.208 us | 21.040 us | 404.5 us |    2 |
    WithSort | 117.2 us | 2.311 us |  2.751 us | 116.5 us |    1 |

```
* [Full benchmark summary](https://raw.githubusercontent.com/jefersonsv/BranchPrediction/master/benchmark.txt)