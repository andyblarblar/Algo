/*using System;
using System.Collections.Generic;
using Algo.Extentions;
using BenchmarkDotNet.Attributes;

namespace Algo.Benchmark
{
    [MemoryDiagnoser]
    public class HeapSortVsSort
    {
        private List<string> bclString;
        private List<string> heapString;
        private List<int> bclInt;
        private List<int> HeapInt;

        public HeapSortVsSort()
        {
            heapString = new List<string> { "asda", "asdasdad", "asdad", "dasdad", "asdasdasd", "sadasdd" };
            bclString = new List<string> { "asda", "asdasdad", "asdad", "dasdad", "asdasdasd", "sadasdd" };
            bclInt = new List<int>();
            HeapInt = new List<int>();

            var ran = new Random();
            for (int i = 0; i < 100; i++)
            {
                bclInt.Add(ran.Next());
                HeapInt.Add(ran.Next());
            }

        }


        [Benchmark]
        public List<string> BclStringSort()
        {
            bclString.Sort();
            return bclString;
        }
        
        [Benchmark]
        public List<string> HeapStringSort()
        {
            heapString.HeapSort();
            return heapString;
        }

        [Benchmark]
        public List<int> BclBigintSort()
        {
            bclInt.Sort();
            return bclInt;
        }
        
        [Benchmark]
        public List<int> HeapBigIntSort()
        {
            HeapInt.HeapSort();
            return HeapInt;
        }


    }
}
*/