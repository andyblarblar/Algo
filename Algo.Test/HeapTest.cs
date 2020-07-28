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

            heap.Insert(1123);
            heap.Insert(21);
            heap.Insert(3);
            heap.Insert(456456);
            heap.Insert(100234);
            heap.Insert(1224);
            
            Console.WriteLine(heap);
            Console.WriteLine(heap.ExtractRoot());
            Assert.AreEqual(100234,heap.Root);
            Console.WriteLine(heap);
            Console.WriteLine(heap.ExtractRoot());
            Assert.AreEqual(1224, heap.Root);
            Console.WriteLine(heap); 
            Console.WriteLine(heap.ExtractRoot());
            Assert.AreEqual(1123, heap.Root);
            Console.WriteLine(heap);
            Console.WriteLine(heap.ExtractRoot());
            Assert.AreEqual(21, heap.Root);
            Console.WriteLine(heap);
            Console.WriteLine(heap.ExtractRoot());
            Assert.AreEqual(3, heap.Root);
            Console.WriteLine(heap);
            Console.WriteLine(heap.ExtractRoot());
            Console.WriteLine(heap);

            heap.Insert(124);
            heap.Insert(23);
            heap.Insert(25);
            heap.Insert(26);
            heap.Insert(27);
            heap.Insert(29);
            heap.Insert(12309);
            Console.WriteLine(heap);
        }
        
        [Test]
        public void PriorityQueueExample()
        { 
            var pq = new PriorityQueue<string>(true);
            pq.Add("hi", 2)
                .Add("hii", 3)
                .Add("hiii", 4)
                .Add("hiiii", 5);

            pq.ChangePriority(("hii", 3), 4);
            Console.WriteLine(pq);
            Console.WriteLine(pq.Poll());
            Console.WriteLine(pq.Poll()); 
            Console.WriteLine(pq.Poll());
            Console.WriteLine(pq.Poll());
        }

        [Test]
        public void PriorityQueueExpands()
        {
            Assert.DoesNotThrow((() =>
            {
                var pq = new PriorityQueue<string>(true, 1);
                pq.Add("hi", 2)
                    .Add("hii", 3)
                    .Add("hiii", 4)
                    .Add("hiiii", 5);
            }));
        }

        [Test]
        public void PriorityQueuePriorityWorks()
        {
            var pq = new PriorityQueue<string>(true);
            pq.Add("hi", 2)
                .Add("hii", 3)
                .Add("hiii", 4)
                .Add("hiiii", 5);

            Assert.AreEqual("hiiii",pq.Poll());
            Console.WriteLine(pq);
            Assert.AreEqual("hiii",pq.Poll());
            Console.WriteLine(pq);
            Assert.AreEqual("hii",pq.Poll());
            Console.WriteLine(pq);
            Assert.AreEqual("hi",pq.Poll());
            Assert.Catch((() => pq.Poll()));
        }

        [Test]
        public void PriorityQueueRemoveWorks()
        {
            var pq = new PriorityQueue<string>(true);
            pq.Add("hi", 2)
                .Add("hii", 3)
                .Add("hiii", 4)
                .Add("hiiii", 5);

            pq.Remove(("hiiii",5));

            Console.WriteLine(pq);
            Assert.AreNotEqual("hiiii",pq.Peek());
        }

        [Test]
        public void PriorityQueueChangeValueWorks()
        {
            var pq = new PriorityQueue<string>(true);
            pq.Add("hi", 2)
                .Add("hii", 3)
                .Add("hiii", 4)
                .Add("hiiii", 5);

            pq.ChangePriority(("hii",3), 4000);
            Console.WriteLine(pq);
            Assert.IsTrue(pq.Contains("hii",4000));
            Assert.AreEqual("hii",pq.Peek());
        }

        [Test]
        public void PriorityQueueClearWorks()
        {
            var pq = new PriorityQueue<string>(true);
            pq.Add("hi", 2)
                .Add("hii", 3)
                .Add("hiii", 4)
                .Add("hiiii", 5);

            pq.Clear();
            Console.WriteLine(pq);
            Assert.Catch(() => pq.Poll());
            Assert.DoesNotThrow(() =>
            {
                pq.Add("sup", 3)
                    .Add("supp", 4)
                    .Add("suppp", 5);
            });
            Assert.AreEqual("suppp",pq.Peek());
            Console.WriteLine(pq);
        }

        [Test]
        public void PriorityQueueCopyWorks()
        {
            var pq = new PriorityQueue<string>(true);
            pq.Add("hi", 2)
                .Add("hii", 3)
                .Add("hiii", 4)
                .Add("hiiii", 5);

            var arr = pq.ToList();

            Assert.AreEqual("hiiii", arr[0].Data);
        }

        [Test]
        public void PriorityQueueMinExpands()
        {
            Assert.DoesNotThrow((() =>
            {
                var pq = new PriorityQueue<string>(false, 1);
                pq.Add("hi", 2)
                    .Add("hii", 3)
                    .Add("hiii", 4)
                    .Add("hiiii", 5);
            }));
        }

        [Test]
        public void PriorityQueueMinPriorityWorks()
        {
            var pq = new PriorityQueue<string>(false);
            pq.Add("hi", 2)
                .Add("hii", 3)
                .Add("hiii", 4)
                .Add("hiiii", 5);
            
            Assert.AreEqual("hi", pq.Poll());
            Console.WriteLine(pq);
            Assert.AreEqual("hii", pq.Poll());
            Console.WriteLine(pq);
            Assert.AreEqual("hiii", pq.Poll());
            Assert.AreEqual("hiiii", pq.Poll());
            Assert.Catch((() => pq.Poll()));
        }

        [Test]
        public void PriorityQueueMinRemoveWorks()
        {
            var pq = new PriorityQueue<string>(false);
            pq.Add("hi", 2)
                .Add("hii", 3)
                .Add("hiii", 4)
                .Add("hiiii", 5);

            pq.Remove(("hiii", 4));//TODO violates heap, must move to right child of root

            Console.WriteLine(pq);
            Assert.AreEqual("hi", pq.Peek());
            Console.WriteLine(pq.Poll());
            Console.WriteLine(pq.Poll());
            Console.WriteLine(pq.Poll());
        }

        [Test]
        public void PriorityQueueMinChangeValueWorks()
        {
            var pq = new PriorityQueue<string>(false);
            pq.Add("hi", 2)
                .Add("hii", 3)
                .Add("hiii", 4)
                .Add("hiiii", 5);

            pq.ChangePriority(("hii", 3), 4000);
            Console.WriteLine(pq);
            Assert.IsTrue(pq.Contains("hii", 4000));
            Assert.AreEqual("hi", pq.Peek());
        }

        [Test]
        public void PriorityQueueMinClearWorks()
        {
            var pq = new PriorityQueue<string>(false);
            pq.Add("hi", 2)
                .Add("hii", 3)
                .Add("hiii", 4)
                .Add("hiiii", 5);

            pq.Clear();
            Console.WriteLine(pq);
            Assert.Catch(() => pq.Poll());
            Assert.DoesNotThrow(() =>
            {
                pq.Add("sup", 3)
                    .Add("supp", 4)
                    .Add("suppp", 5);
            });
            Assert.AreEqual("sup", pq.Peek());
            Console.WriteLine(pq);
        }

        [Test]
        public void PriorityQueueMinCopyWorks()
        {
            var pq = new PriorityQueue<string>(false);
            pq.Add("hi", 2)
                .Add("hii", 3)
                .Add("hiii", 4)
                .Add("hiiii", 5);

            var arr = pq.ToList();

            Assert.AreEqual("hi", arr[0].Data);
        }
        [Test]
        public void NodeComparisonsWork()
        {
            var node = new PriorityQueue<int>.QueueNode<string>("hi",2);
            var node2 = new PriorityQueue<int>.QueueNode<string>("hi",3);
            var node3 = new PriorityQueue<int>.QueueNode<string>(null,0);

            //smaller
            Assert.AreEqual(-1,node.CompareTo(node2));
            //larger
            Assert.AreEqual(1,node2.CompareTo(node));
            //equal
            Assert.AreEqual(0, node2.CompareTo(node2));
            //null doesn't break things
            Assert.DoesNotThrow((() => node2.CompareTo(node3)));
        }



    }
}
