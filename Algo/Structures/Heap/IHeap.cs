using System;

namespace Algo.Structures.Heap
{
    public interface IHeap<T> where T : IComparable<T>
    {
        /// <summary>
        /// The current maximum amount of nodes allowed in heap.
        /// </summary>
        int Capacity { get; }

        /// <summary>
        /// Current number of nodes in heap.
        /// </summary>
        int HeapSize { get; }

        /// <summary>
        /// Returns the root of the heap
        /// </summary>
        T Root { get; }

        /// <summary>
        /// Changes the key at index to a new value, then restores heap.
        /// The new value must be different from the old value. 
        /// </summary>
        /// <returns>The new index of the key.</returns>
        int ChangeValue(int index, T newVal);

        /// <summary>
        /// Removes all elements in the heap for reuse.
        /// </summary>
        void Clear();

        /// <summary>
        /// Searches for the presence of a key in heap.
        /// </summary>
        bool Contains(T obj);

        /// <summary>
        /// Copies the array representation of this heap to the passed array
        /// </summary>
        void CopyTo(T[] arr, int index);

        /// <summary>
        /// Expands this heaps capacity to a new size.
        /// </summary>
        /// <param name="newSize">The new size of the heap. Must be larger than current capacity</param>
        /// <exception cref="InvalidOperationException">Thrown if newSize is an invalid size.</exception>
        void ExpandSizeTo(int newSize);

        /// <summary>
        /// Returns and deletes the root of the heap.
        /// </summary>
        /// <exception cref="InvalidOperationException">thrown if heap is empty</exception>
        T ExtractRoot();

        /// <summary>
        /// Inserts a new key into the heap.
        /// </summary>
        /// <returns>The index of the inserted key.</returns>
        /// <exception cref="OutOfMemoryException">thrown if heap is out of space.</exception>
        int Insert(T key);

        /// <summary>
        /// Removes the key at index and fixes the heap.
        /// </summary>
        void Remove(int index);

        /// <summary>
        /// Searches the heap for index of passed key.
        /// </summary>
        /// <param name="obj">The key to search for.</param>
        /// <returns>The index of the key if found, otherwise -1.</returns>
        int Search(T obj);
    }
}