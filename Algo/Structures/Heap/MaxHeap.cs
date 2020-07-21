using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Algo.Structures.Heap
{
    public class MaxHeap
    {
        private int[] arr;
        public int HeapSize { get; private set; }

        /// <summary>
        /// Returns the maximum key in the heap
        /// </summary>
        public int Max => arr[0];

        public MaxHeap(int[] data)
        {
            arr = data;
        }

        private int Parent(int i) { return (i - 1) / 2; }

        // to get index of left child of node at index i 
        private int Left(int i) { return (2 * i + 1); }

        // to get index of right child of node at index i 
        private int Right(int i) { return (2 * i + 2); }

        public MaxHeap InsertKey(int key)
        {
            if (HeapSize == arr.Length)
            {
                throw new InternalBufferOverflowException();
            }

            HeapSize++;
            var i = HeapSize - 1;
            arr[i] = key;

            //Max-heapify
            while (i != 0 && arr[Parent(i)] < arr[i])
            {
                (arr[i], arr[Parent(i)]) = (arr[Parent(i)], arr[i]);
                i = Parent(i);
            }

            return this;
        }

        public MaxHeap DecreaseKey(int i, int val)
        {
            arr[i] = val;

            //Max-heapify
            while (i != 0 && arr[Parent(i)] < arr[i])
            {
                (arr[i], arr[Parent(i)]) = (arr[Parent(i)], arr[i]);
                i = Parent(i);
            }

            return this;
        }

        public int ExtractMax()
        {
            if (HeapSize <= 0)
            {
                throw new InvalidOperationException();
            }

            if (HeapSize == 1)
            {
                HeapSize--;
                return arr[0];
            }

            var root = arr[0];
            arr[0] = arr[HeapSize - 1];
            HeapSize--;
            MaxHeapify(0);

            return root;
        }

        public MaxHeap DeleteKey(int i)
        {
            DecreaseKey(i, int.MaxValue);
            ExtractMax();

            return this;
        }

        /// <summary>
        /// MaxHeapifys the tree at parent i
        /// </summary>
        /// <param name="i"></param>
        private void MaxHeapify(int i)
        {
            while (true)
            {
                var l = Left(i);
                var r = Right(i);
                var largest = i;

                if (l < HeapSize && arr[l] > arr[i]) largest = l;
                if (r < HeapSize && arr[r] > arr[largest]) largest = r;

                if (largest != i)
                {
                    (arr[i], arr[largest]) = (arr[largest], arr[i]);
                    i = largest;
                    continue;
                }

                break;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append('[');

            var acc = 0;

            foreach (var i in arr)
            {
                if (acc == HeapSize) break;

                sb.Append(i);
                sb.Append(',');
                acc++;
            }

            sb.Append(']');

            return sb.ToString();
        }
    }
}
