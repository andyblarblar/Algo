using System;
using System.Collections.Generic;
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
            var heap = new MaxHeap<int>(new int[]{1,566,3,4,123123,0,0,0});
           
            Console.WriteLine(heap);

            heap.DeleteKey(0);

            Console.WriteLine(heap);
            Assert.AreEqual(4,heap.Max);

        }

        [Test]
        public void NonIntTest()
        {
            var heap = new MaxHeap<string>(new string[20]);

            heap.InsertKey("sup")
                .InsertKey("supp")
                .InsertKey("suppp")
                .InsertKey("hi");

            Console.WriteLine(heap);
            Assert.AreEqual("suppp", heap.ExtractMax());
            Console.WriteLine(heap);

            var arr = new List<int>{1,2,4,7,100,34,566};

            var heap2 = new MaxHeap<int>(arr.ToArray());

            Console.WriteLine(heap2);


            arr.ForEach(i => Console.Write(i+","));
            Console.WriteLine();

            arr.HeapSort();

            arr.ForEach(i => Console.Write(i+","));


        }



    }
}
