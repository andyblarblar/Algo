using System;
using System.Collections.Generic;
using System.Text;
using Algo.Structures.Heap;

namespace Algo.Extentions
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// The worst heap-sort known to man.
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



    }
}
