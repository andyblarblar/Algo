using System;
using System.Collections.Generic;
using System.Text;

namespace Algo.Structures.Heap
{
    public class PriorityQueue<T> 
    {
        private readonly MaxHeap<QueueNode<T>> _heap;

        public PriorityQueue(int size)
        {
            _heap = new MaxHeap<QueueNode<T>>(size);
        }

        /// <summary>
        /// Inserts a new element into the priority queue with given priority.
        /// </summary>
        public PriorityQueue<T> Add(T element, int priority)
        {
            _heap.Insert(new QueueNode<T>(element, priority));

            return this;
        }

        /// <summary>
        /// Returns the highest priority item
        /// </summary>
        public T Peek() => _heap.Max.Data;

        /// <summary>
        /// Returns and removes the highest priority item
        /// </summary>
        public T Poll() => _heap.ExtractMax().Data;

        /// <summary>
        /// Removes all the contents of this queue
        /// </summary>
        public void Clear() => _heap.Clear();

        /// <summary>
        /// Copies the array representation of this priority queues heap to a new array.
        /// This will be missing priority data. 
        /// </summary>
        public void CopyTo(IList<T> list, int index)
        { 
            var temp = new List<QueueNode<T>>();
            _heap.CopyTo(temp,0);

            var acc = 0;
            foreach (var i in temp.ConvertAll(e => e.Data))
            {
                list[index + acc] = i;
                acc++;
            }
        }

        public bool Contains<T>(T obj,int priority) => _heap.Contains(new QueueNode<T>(obj,priority));

        /// <summary>
        /// Copies the array representation of this priority queues heap to a new List.
        /// This will be missing priority data. 
        /// </summary>
        public List<T> ToList()
        {
            var temp = new List<T>();
            CopyTo(temp,0);
            return temp;
        }

        /// <summary>
        /// Expands this priority queue's capacity to newSize.
        /// </summary>
        /// <param name="newSize"></param>
        public void ExpandSizeTo(int newSize) => _heap.ExpandSizeTo(newSize);

        public override string ToString()
        {
            return _heap.ToString();
        }

        public readonly struct QueueNode<T> : IComparable<QueueNode<T>> 
        {
            public QueueNode(T data, int priority)
            {
                Data = data;
                Priority = priority;
            }

            public T Data { get; }
            public int Priority { get; }

            public int CompareTo(QueueNode<T> other)
            {
                return Priority.CompareTo(other.Priority);
            }

            public override string ToString()
            {
                return Data?.ToString();
            }

        }

    }
    

}
