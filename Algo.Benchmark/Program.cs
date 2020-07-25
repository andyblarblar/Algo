
using System.IO;
using BenchmarkDotNet.Running;

namespace Algo.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<HeapSortVsSort>();
        }
    }
}
