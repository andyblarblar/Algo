using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Algo.Extentions;
using Algo.Structures.Heap;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Algo.Test
{
    class HeapTest
    {
        [Test]
        public void Example()
        {
            var heap = new MaxHeap<int>(10);

            heap.Insert(1123)
                .Insert(21)
                .Insert(3)
                .Insert(456456)
                .Insert(100234)
                .Insert(1224)
                ;

            Console.WriteLine(heap);
            Console.WriteLine(heap.ExtractMax());
            Assert.AreEqual(100234,heap.Max);
            Console.WriteLine(heap);
            Console.WriteLine(heap.ExtractMax());
            Assert.AreEqual(1224, heap.Max);
            Console.WriteLine(heap); 
            Console.WriteLine(heap.ExtractMax());
            Assert.AreEqual(1123, heap.Max);
            Console.WriteLine(heap);
            Console.WriteLine(heap.ExtractMax());
            Assert.AreEqual(21, heap.Max);
            Console.WriteLine(heap);
            Console.WriteLine(heap.ExtractMax());
            Assert.AreEqual(3, heap.Max);
            Console.WriteLine(heap);
            Console.WriteLine(heap.ExtractMax());
            Console.WriteLine(heap);

            heap.Insert(124)
                .Insert(23)
                .Insert(25)
                .Insert(26)
                .Insert(27)
                .Insert(29)
                .Insert(12309);
            Console.WriteLine(heap);
        }
        
        [Test]
        public void PriorityQueueTest()
        {
            var pq = new PriorityQueue<string>(1000);
            pq.Add("hi", 2)
                .Add("hii", 3)
                .Add("hii", 3)
                .Add("hiii", 4)
                .Add("hiiii", 5);

        }


    }
}
