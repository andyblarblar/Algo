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
    public class MaxHeap<T> : IHeap<T> where T : IComparable<T>
    {
        /// <summary>
        /// The array representation of this heap.
        /// </summary>
        private T[] _arr;

        /// <summary>
        /// Sets weather the whole internal array is printed in ToString. 
        /// </summary>
        private readonly bool _debug;

        /// <summary>
        /// Current number of nodes in heap.
        /// </summary>
        public int HeapSize { get; private set; }

        /// <summary>
        /// Returns the maximum key in the heap.
        /// </summary>
        public T Root => _arr[0];

        /// <summary>
        /// The current maximum amount of nodes allowed in heap.
        /// </summary>
        public int Capacity => _arr.Length;

        /// <summary>
        /// Creates a new empty max-heap.
        /// </summary>
        /// <param name="size">The starting size of the heap.</param>
        /// <param name="debug">Prints the whole internal array if set.</param>
        public MaxHeap(int size, bool debug = false)
        {
            this._debug = debug;
            _arr = new T[size];
            HeapSize = 0;
        }

        /// <summary>
        /// Finds the index of the parent node of node at index i.
        /// </summary>
        public static int Parent(int i) { return (i - 1) / 2; }

        /// <summary>
        /// Finds the index of the Left child node of node at index i.
        /// </summary>
        public static int Left(int i) { return (2 * i + 1); }

        /// <summary>
        /// Finds the index of the Right child node of node at index i.
        /// </summary>
        public static int Right(int i) { return (2 * i + 2); }

        /// <summary>
        /// Inserts a new key into the heap.
        /// </summary>
        /// <returns>The index of the inserted key.</returns>
        /// <exception cref="OutOfMemoryException">thrown if heap is out of space.</exception>
        public int Insert(T key)
        {
            if (HeapSize == Capacity)
            {
                throw new OutOfMemoryException("Heap is out of space");
            }

            //insert elm at end of heap
            _arr[HeapSize] = key;
          
            var index = BubbleUp(HeapSize);

            //increment here to avoid messing up index operations
            HeapSize++;
            return index;
        }

        /// <summary>
        /// Changes the key at index to a new value, then restores heap.
        /// The new value must be different from the old value. 
        /// </summary>
        /// <returns>The new index of the key.</returns>
        public int ChangeValue(int index, T newVal)
        {
            var biggerThanOldVal = newVal.CompareTo(_arr[index]) > 0;

            _arr[index] = newVal;

            //Restore heap invariant
            return biggerThanOldVal
                ? BubbleUp(index)
                : MaxHeapify(index);
        }

        /// <summary>
        /// Removes the key at index and fixes the heap.
        /// </summary>
        public void Remove(int index)
        {
            _arr[index] = _arr[--HeapSize];
            MaxHeapify(index);
        }

        /// <summary>
        /// Bubbles up the selected node.
        /// </summary>
        /// <returns>The new index of the key.</returns>
        private int BubbleUp(int startingIndex)
        {
            var acc = startingIndex;
            while (_arr[acc].CompareTo(_arr[Parent(acc)]) > 0)
            {
                (_arr[acc], _arr[Parent(acc)]) = (_arr[Parent(acc)], _arr[acc]);
                acc = Parent(acc);
            }

            return acc;
        }

        /// <summary>
        /// Returns and deletes the largest key in the heap.
        /// </summary>
        /// <exception cref="InvalidOperationException">thrown if heap is empty</exception>
        public T ExtractRoot()
        {
            if (HeapSize == 0) throw new InvalidOperationException("heap is empty");

            var root = _arr[0];
            _arr[0] = _arr[--HeapSize];
            MaxHeapify(0);
            return root;
        }

        /// <summary>
        /// Searches the heap for index of passed key.
        /// </summary>
        /// <param name="obj">The key to search for.</param>
        /// <returns>The index of the key if found, otherwise -1.</returns>
        public int Search(T obj)
        {
            if (obj.CompareTo(_arr[0]) > 0) return -1;

            for (var i = 0; i < _arr.Length; i++)//TODO optimize? currently O(n) worst case. 
            {
                if (_arr[i].CompareTo(obj) == 0) return i;
            }

            return -1;
        }

        /// <summary>
        /// Searches for the presence of a key in heap.
        /// </summary>
        public bool Contains(T obj) => Search(obj) != -1;

        /// <summary>
        /// Heapifys the subtree at index. "Bubble down".
        /// Assumes both subtrees are already valid heaps.
        /// </summary>
        /// <returns>The new index of the node previously at index.</returns>
        private int MaxHeapify(int index)
        {
            while (true)
            {
                //If leaf, then break
                if (Left(index) > HeapSize)
                {
                    break;
                }

                //Path if right child exists 
                if (Right(index) < HeapSize)
                {
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
                //Path if only left child exists
                else
                {
                    //if child is greater than parent,
                    if (_arr[index].CompareTo(_arr[Left(index)]) < 0)
                    {
                        //then swap and run again
                        (_arr[index], _arr[Left(index)]) = (_arr[Left(index)], _arr[index]);
                        index = Left(index);
                        continue;
                    }

                    break;
                }
            }


            return index;
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
        /// Expands this heaps capacity to a new size.
        /// </summary>
        /// <param name="newSize">The new size of the heap. Must be larger than current capacity</param>
        /// <exception cref="InvalidOperationException">Thrown if newSize is an invalid size.</exception>
        public void ExpandSizeTo(int newSize)
        {
            if (newSize < Capacity) throw new InvalidOperationException("Cannot shrink heap.");
            var newArr = new T[newSize];
            _arr.CopyTo(newArr, 0);
            _arr = newArr;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append('[');

            var acc = 0;

            foreach (var i in _arr)
            {
                if (!_debug && acc == HeapSize) break;

                sb.Append(i);
                sb.Append(',');
                acc++;
            }

            sb.Append(']');

            return sb.ToString();
        }

        /// <summary>
        /// Copies the array representation of this heap to the passed array
        /// </summary>
        public void CopyTo(T[] arr, int index) => _arr.CopyTo(arr, index);

    }
}
