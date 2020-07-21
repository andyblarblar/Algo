using System;
using System.Collections.Generic;
using System.Text;
using Algo.Structures.Heap;
using NUnit.Framework;

namespace Algo.Test
{
    class HeapTest
    {
        [Test]
        public void Example()
        {
            var heap = new MaxHeap(new int[10]);

            heap.InsertKey(1)
                .InsertKey(2)
                .InsertKey(3)
                .InsertKey(5)
                .InsertKey(1000);

            Console.WriteLine(heap);
            Console.WriteLine(heap.ExtractMax());
            Console.WriteLine(heap);
            Console.WriteLine(heap.ExtractMax());
            Console.WriteLine(heap);
            Console.WriteLine(heap.HeapSize);

        }


    }
}
