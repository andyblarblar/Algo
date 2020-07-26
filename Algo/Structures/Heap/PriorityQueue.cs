using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Algo.Extentions;

namespace Algo.Structures.Heap
{
    /// <summary>
    /// A binary heap based dynamically expanding priority queue.
    /// </summary>
    public class PriorityQueue<T> 
    {
        /// <summary>
        /// The internal heap.
        /// </summary>
        private readonly MaxHeap<QueueNode<T>> _heap;

        /// <summary>
        /// Creates a new priority queue.
        /// </summary>
        /// <param name="size">The starting size of this queue</param>
        public PriorityQueue(int size = 10)
        {
            _heap = new MaxHeap<QueueNode<T>>(size);
        }
        
        /// <summary>
        /// Creates a new priority queue containing a set of key-priority pairs.
        /// </summary>
        public PriorityQueue(IEnumerable<(T, int)> keys)
        {
            var tuples = keys.ToList();
            _heap = new MaxHeap<QueueNode<T>>(tuples.Count() + 10);
            AddRange(tuples);
        }

        /// <summary>
        /// Inserts a new element into the priority queue with given priority.
        /// </summary>
        public PriorityQueue<T> Add(T element, int priority)
        {
            //Expand heap size if Capacity reached. 
            if(_heap.HeapSize == _heap.Capacity) ExpandSizeTo(_heap.Capacity + 10);

            var index = _heap.Insert(new QueueNode<T>(element, priority));
            return this;
        }
        
        /// <summary>
        /// Inserts the range of key priority pairs to the queue.
        /// </summary>
        public PriorityQueue<T> AddRange(IEnumerable<(T, int)> keys)
        {
            //Expand heap size if Capacity reached. 
            if(_heap.HeapSize == _heap.Capacity) ExpandSizeTo(_heap.Capacity + 10 + keys.Count());

            foreach (var (data, pri) in keys)
            {
                _heap.Insert(new QueueNode<T>(data,pri));
            }

            return this;
        }

        /// <summary>
        /// Returns the highest priority item.
        /// </summary>
        public T Peek() => _heap.Max.Data;

        /// <summary>
        /// Returns and removes the highest priority item.
        /// </summary>
        public T Poll()
        {
            return _heap.ExtractMax().Data;
        }

        /// <summary>
        /// Changes the priority of a node oldVal to a new priority.
        /// </summary>
        /// <param name="oldVal">the value to change in (data,priority)</param>
        /// <param name="newPri">The new priority</param>
        /// <exception cref="ArgumentException">Thrown if the key is not present in this priority queue.</exception>
        public void ChangePriority((T,int) oldVal,int newPri)
        {
            var (data, pri) = oldVal;
            var old = new QueueNode<T>(data, pri);

            var index = _heap.Search(old);

            if (index < 0) throw new ArgumentException($"key: ({data},{pri}) was not found in the heap.");
            _heap.ChangeValue(index, new QueueNode<T>(data, newPri));
        }

        /// <summary>
        /// Removes the first found key matching oldVal.
        /// </summary>
        /// <param name="oldVal">the item to find, (data,priority)</param>
        /// <exception cref="ArgumentException">Thrown if the key is not present in this priority queue.</exception>
        public void Remove((T, int) oldVal)
        {
            var (data, pri) = oldVal;
            var val = new QueueNode<T>(data, pri);

            var index =_heap.Search(val);

            if(index < 0) throw new ArgumentException($"key: ({data},{pri}) was not found in the heap.");
            _heap.Remove(index);
        }

        /// <summary>
        /// Removes all the contents of this queue.
        /// </summary>
        public void Clear() => _heap.Clear();

        /// <summary>
        /// Copies the array representation of this priority queue's heap to a new array.
        /// </summary>
        public void CopyTo(QueueNode<T>[] list, int index) => _heap.CopyTo(list, index);

        public bool Contains(T obj,int priority) => _heap.Contains(new QueueNode<T>(obj,priority));

        /// <summary>
        /// Copies the array representation of this priority queues heap to a new List.
        /// </summary>
        public List<QueueNode<T>> ToList()
        {
            var arr = new QueueNode<T>[_heap.Capacity];
            _heap.CopyTo(arr,0);
            return arr.ToList();
        }

        /// <summary>
        /// Expands this priority queue's capacity to newSize.
        /// Note that the queue will expand automaticly, although use of this
        /// method before batch insertions can boost performance.
        /// </summary>
        public void ExpandSizeTo(int newSize) => _heap.ExpandSizeTo(newSize);

        public override string ToString()
        {
            return _heap.ToString();
        }

        /// <summary>
        /// A node in a priority queue. Simply wraps a value and its priority.
        /// </summary>
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
                if((Data?.Equals(other.Data) ?? false) && Priority == other.Priority)
                {
                    return 0;
                }

                return Priority.CompareTo(other.Priority);
            }

            public override string ToString()
            {
                return $"({Data?.ToString() ?? "nil"}, {Priority})";
            }

        }

    }
    

}
