using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Algo.Extentions;

namespace Algo.Structures.Heap
{
    /// <summary>
    /// A binomial max-heap
    /// </summary>
    /// <typeparam name="T">type of key used in this heap.</typeparam>
    public class MaxHeap<T> where T : IComparable<T>
    {
        private T[] _arr;
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

            BubbleUp(HeapSize);

             //increment here to avoid messing up index operations
             HeapSize++;
             return this;
        }

        /// <summary>
        /// Changes the key at index to a new value, then restores heap.
        /// The new value must be different from the old value. 
        /// </summary>
        public void ChangeValue(int index, T newVal)
        {
            var biggerThanOldVal = newVal.CompareTo(_arr[index]) > 0;

            _arr[index] = newVal;

            //Restore heap invarient
            if (biggerThanOldVal)
            {
                BubbleUp(index);
            }
            else
            {
                MaxHeapify(index);
            }

        }

        /// <summary>
        /// Removes the key at index and fixes the heap.
        /// </summary>
        public MaxHeap<T> Remove(int index) 
        {
            ChangeValue(index, default!);

            return this;
        }

        /// <summary>
        /// Bubbles up the selected node.
        /// </summary>
        private void BubbleUp(int startingIndex)
        {
            var acc = startingIndex;
            while (_arr[acc].CompareTo(_arr[Parent(acc)]) > 0)
            {
                (_arr[acc], _arr[Parent(acc)]) = (_arr[Parent(acc)], _arr[acc]);
                acc = Parent(acc);
            }
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
        /// Searches the heap for the index of val. If val does not exist, then a negitive number is returned.
        /// </summary>
        public int Search(T val) => _arr.BinarySearch(val);

        public bool Contains<T>(T obj) => _arr.Any(i => i.Equals(obj));

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

        /// <summary>
        /// Removes all elements in the heap for reuse.
        /// </summary>
        public void Clear()
        {
            for (var i = 0; i < _arr.Length; i++)
            {
                _arr[i] = default!;
            }

            HeapSize = 0;
        }

        /// <summary>
        /// Expands this heaps capacity to newSize
        /// </summary>
        /// <param name="newSize"></param>
        public void ExpandSizeTo(int newSize)
        {
            var newArr = new T[newSize];
            _arr.CopyTo(newArr,0);
            _arr = newArr;
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

        /// <summary>
        /// Copies the array representation of this heap to the passed list. 
        /// </summary>
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
