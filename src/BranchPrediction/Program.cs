using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Running;

namespace BranchPrediction
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

#if (DEBUG)
            var debug = new BranchPredictionRunner();
            debug.WithoutSort();
#else
            BenchmarkRunner.Run<BranchPredictionRunner>();
#endif
        }

        [ClrJob(isBaseline: false)]
        [RPlotExporter, RankColumn]
        public class BranchPredictionRunner
        {
            private static readonly Random getrandom = new Random();
            private readonly int arraySize;
            private readonly int middle;
            private readonly int[] unorderedData;
            private readonly int[] orderedData;

            public BranchPredictionRunner()
            {
                arraySize = UInt16.MaxValue;
                middle = arraySize / 2;
                var data = new int[arraySize];
                unorderedData = new int[arraySize];
                orderedData = new int[arraySize];

                for (uint c = 0; c < arraySize; ++c)
                    data[c] = getrandom.Next(arraySize);

                data.CopyTo(unorderedData, 0);
                data.OrderBy(o => o).ToArray().CopyTo(orderedData, 0);
            }

            [Benchmark]
            public void WithoutSort()
            {
                long sum = 0;
                for (uint c = 0; c < arraySize; ++c)
                {
                    if (unorderedData[c] >= middle)
                        sum += unorderedData[c];
                }
            }

            [Benchmark]
            public void WithSort()
            {
                long sum = 0;
                for (uint c = 0; c < arraySize; ++c)
                {
                    if (orderedData[c] >= middle)
                        sum += orderedData[c];
                }
            }
        }
    }
}
