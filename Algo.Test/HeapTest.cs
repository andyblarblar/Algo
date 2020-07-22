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
            var heap = new MaxHeap<int>(new int[10]);

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

        [Test]
        public void Insert()
        {
            var heap = new MaxHeap<int>(new int[10]);

            var heap2 = new MaxHeap<int>(new int[] { 1, 5, 3, 4, 0, 0, 0, 0 });

            Console.WriteLine(heap2);
            heap2.InsertKey(2);
            Console.WriteLine(heap2);

            heap.InsertKey(1)
                .InsertKey(4)
                .InsertKey(2)
                .InsertKey(3);

            Assert.AreEqual(4,heap.HeapSize);

            Assert.AreEqual(4, heap.Max);
        }

        [Test]
        public void Remove()
        {
            var heap = new MaxHeap<int>(new int[]{1,5,3,4,0,0,0,0});

            heap.DeleteKey(MaxHeap<int>.Parent(2));

            Console.WriteLine(heap);
            Assert.AreEqual(4,heap.Max);
        }



    }
}
