using System;
using System.Collections.Generic;
using System.Text;
using Algo.Structures.Heap;

namespace Algo.Extentions
{
    public static class ArrayExtensions
    {
        public static void HeapSort<T>(this IList<T> arr) where T : IComparable<T>
        {
            var i = 0;
            while (true)
            {
                var l = MaxHeap<int>.Left(i);
                var r = MaxHeap<int>.Right(i);
                var largest = i;

                if (l < arr.Count && arr[l].CompareTo(arr[i]) == 1) largest = l;
                if (r < arr.Count && arr[r].CompareTo(arr[largest]) == 1) largest = r;

                if (largest != i)
                {
                    (arr[i], arr[largest]) = (arr[largest], arr[i]);
                    i = largest;
                    continue;
                }

                break;
            }

        }



    }
}
