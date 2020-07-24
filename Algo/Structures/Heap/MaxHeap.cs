using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Algo.Structures.Heap
{
    
    public class MaxHeap<T> where T : IComparable<T>
    {
        private readonly T[] _arr;
        public int HeapSize { get; private set; }

        /// <summary>
        /// Returns the maximum key in the heap
        /// </summary>
        public T Max => _arr[0];

        public int Capacity => _arr.Length;

        public MaxHeap(int size)
        {
            _arr = new T[size];
            HeapSize = 0;
        }

        public static int Parent(int i) { return (i - 1) / 2; }

        public static int Left(int i) { return (2 * i + 1); }

        public static int Right(int i) { return (2 * i + 2); }

        /// <summary>
        /// Inserts a new key to the heap.
        /// </summary>
        /// <exception cref="OutOfMemoryException">thrown if heap is out of space.</exception>
        public MaxHeap<T> Insert(T key)
        {
             if (HeapSize == Capacity)
             {
                throw new OutOfMemoryException("Heap is out of space");
             }

             //insert elm at end of heap
             if (HeapSize == 0)
             {
                 _arr[HeapSize] = key;
             }
             else
             {
                 _arr[HeapSize] = key;
             }

             //Bubble up
             var acc = HeapSize;
             while (_arr[acc].CompareTo(_arr[Parent(acc)]) > 0)
             {
                 (_arr[acc], _arr[Parent(acc)]) = (_arr[Parent(acc)], _arr[acc]);
                 acc = Parent(acc);
             }
             //increment here to avoid messing up index operations
             HeapSize++;
             return this;
        }

        /// <summary>
        /// Returns and deletes the root of this heap
        /// </summary>
        /// <exception cref="InvalidOperationException">thrown if heap is empty</exception>
        public T ExtractMax()
        {
            if(HeapSize == 0) throw new InvalidOperationException("heap is empty");

            var root = _arr[0];
            _arr[0] = _arr[HeapSize];
            MaxHeapify(0);
            HeapSize--;
            return root;
        }

        /// <summary>
        /// Heapifys the subtree at index. "Bubble down"
        /// </summary>
        private void MaxHeapify(int index)
        {
            while (true)
            {
                //If leaf, then break
                if (Left(index) > HeapSize)
                {
                    break;
                }

                //if either child is greater than parent,
                if (_arr[index].CompareTo(_arr[Left(index)]) < 0 || _arr[index].CompareTo(_arr[Right(index)]) < 0)
                {
                    //then swap the larger child with parent and run again
                    if (_arr[Left(index)].CompareTo(_arr[Right(index)]) > 0)
                    {
                        (_arr[index], _arr[Left(index)]) = (_arr[Left(index)], _arr[index]);
                        index = Left(index);
                        continue;
                    }
                    else
                    {
                        (_arr[index], _arr[Right(index)]) = (_arr[Right(index)], _arr[index]);
                        index = Right(index);
                        continue;
                    }
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

        public void CopyTo(IList<T> arr, int index)
        {
            var acc = 0;
            foreach (var i in _arr)
            {
                arr[index + acc] = i;
                acc++;
            }

        }

    }
}
