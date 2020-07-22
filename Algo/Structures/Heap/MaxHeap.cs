using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Algo.Structures.Heap
{
    /// <summary>
    /// MaxHeap capible of containing any object that overrides the greater than and less than operators.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MaxHeap<T> 
    {
        private readonly T[] _arr;
        public int HeapSize { get; private set; }

        /// <summary>
        /// Returns the maximum key in the heap
        /// </summary>
        public T Max => _arr[0];

        public MaxHeap(T[] data)
        {
            _arr = data;
            HeapSize = data.Count(i => i != null && !i.Equals(default(T)));
            MaxHeapify(0);
        }

        public static int Parent(int i) { return (i - 1) / 2; }

        // to get index of left child of node at index i 
        public static int Left(int i) { return (2 * i + 1); }

        // to get index of right child of node at index i 
        public static int Right(int i) { return (2 * i + 2); }

        public MaxHeap<T> InsertKey(T key)
        {
            if (HeapSize == _arr.Length)
            {
                throw new InternalBufferOverflowException("heap is at size limit. Try passing a longer array.");
            }

            HeapSize++;
            var i = HeapSize - 1;
            _arr[i] = key;

            //Max-heapify
            while (i != 0 && (dynamic)_arr[Parent(i)] < (dynamic)_arr[i])
            {
                (_arr[i], _arr[Parent(i)]) = (_arr[Parent(i)], _arr[i]);
                i = Parent(i);
            }

            return this;
        }

        public MaxHeap<T> DecreaseKey(int i, T val)
        {
            _arr[i] = val;

            //Max-heapify
            while (i != 0 && (dynamic) _arr[Parent(i)] < (dynamic) _arr[i])
            {
                (_arr[i], _arr[Parent(i)]) = (_arr[Parent(i)], _arr[i]);
                i = Parent(i);
            }

            return this;
        }

        public T ExtractMax()
        {
            if (HeapSize <= 0)
            {
                throw new InvalidOperationException();
            }

            if (HeapSize == 1)
            {
                HeapSize--;
                return _arr[0];
            }

            var root = _arr[0];
            _arr[0] = _arr[HeapSize - 1];
            HeapSize--;
            MaxHeapify(0);

            return root;
        }

        public MaxHeap<T> DeleteKey(int i)
        {
            DecreaseKey(i, default(T));
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

                if (l < HeapSize && (dynamic)_arr[l] > (dynamic)_arr[i]) largest = l;
                if (r < HeapSize && (dynamic) _arr[r] > (dynamic) _arr[largest]) largest = r;

                if (largest != i)
                {
                    (_arr[i], _arr[largest]) = (_arr[largest], _arr[i]);
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

            foreach (var i in _arr)
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
