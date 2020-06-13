using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Algo.Structures.List;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;


namespace Algo.Benchmark
{
    [MemoryDiagnoser]
    public class AlgoListVSList
    {
        private readonly LinkedList<int> BCLListofInt = new LinkedList<int>();
        private readonly SLList<int> AlgoListofInt = new SLList<int>();

        public AlgoListVSList()
        {
        }

        [Benchmark]
        public LinkedList<int> FillBclWithInt()
        {
            for (var i = 0; i < 10; i++)
            {
                BCLListofInt.AddLast(i);
            }

            return BCLListofInt;
        }

        [Benchmark]
        public SLList<int> FillAlgoWithInt()
        {
            for (var i = 0; i < 10; i++)
            {
                AlgoListofInt.Add(i);
            }

            return AlgoListofInt;
        }

        [Benchmark]
        public int ReadBclWithInt()
        {
            var ret=0;
            ret = BCLListofInt.Last.Value;
            
            return ret;
        }

        [Benchmark]
        public int ReadAlgoWithInt()
        {
            var ret = 0;
            ret = AlgoListofInt.Pop().UnwrapSuccess();
            

            return ret;
        }



    }
}
