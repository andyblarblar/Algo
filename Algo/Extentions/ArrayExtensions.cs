using System;
using System.Collections.Generic;
using System.Text;
using Algo.Structures.Heap;

namespace Algo.Extentions
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Sorts the list from highest to lowest using a heap.
        /// </summary>
        public static void HeapSort<T>(this IList<T> arr) where T : IComparable<T>
        {
            var heap = new MaxHeap<T>(arr.Count);
            foreach (var comparable in arr)
            {
                heap.Insert(comparable);
            }
         
            heap.CopyTo(arr,0);
        }

        /// <summary>
        /// Applies action across the collection.
        /// </summary>
        public static void ForEach<T>(this IList<T> arr, Action<T> action)
        {
            for (var i = 0; i < arr.Count; i++)
            {
                action(arr[i]);
            }

        }



    }
}
