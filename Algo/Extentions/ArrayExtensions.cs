using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algo.Structures.Heap;

namespace Algo.Extentions
{
    public static class ArrayExtensions
    {

        /// <summary>
        /// Applies action across the collection.
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> arr, Action<T> action)
        {
            foreach (var t in arr)
            {
                action(t);
            }
        }

        ///<summary>
        ///Extenstion of:
        /// <see cref="Array.BinarySearch(Array, object)"/>
        /// </summary>
        public static int BinarySearch<T>(this T[] arr, T obj) => Array.BinarySearch<T>(arr, obj);






    }
}
