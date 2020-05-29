using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algo.Structures.List
{
    
    /// <summary>
    /// A naive mutable singly linked list
    /// </summary>
    public static class List
    {
        public class Node<T> : IEnumerable<T>
        {
            public T Data { get; set; }
            public Node<T> Rest { get; set; }

            public int Length => this.Count() - 1;

            public Node(T data, Node<T> next = null)
            {
                Data = data;
                Rest = next;
            }

            public Node()
            {
            }

            /// <summary>
            /// Returns (head,tail) for use in pattern matching
            /// </summary>
            public static (Node<T>, Node<T>) operator ~(Node<T> node) => (node,node.Rest);

            /// <summary>
            /// Appends rhs to the end of lhs.
            /// </summary>
            public static Node<T> operator +(Node<T> lhs, Node<T> rhs) => lhs.GetLastNode().Rest = rhs;

            /// <summary>
            /// Returns the last node in the list 
            /// </summary>
            /// <remarks>O(N) where N is the size of the list</remarks>
            public Node<T> GetLastNode()
            {
                static Node<T> RecLastNode(Node<T> node)
                {
                    while (true)
                    {
                        if (node.Rest is null) return node;
                        node = node.Rest;
                    }
                }

                return RecLastNode(this);
            }

            public T this[uint i]
            {
                get
                {
                    if (i > this.Count() - 1) throw new IndexOutOfRangeException();
                        
                    var j = 0;
                    var acc = this;

                    while (i != j) 
                    {
                        acc = acc.Rest;
                        j++;
                    }

                    return acc.Data;
                }
                set
                {
                    if (i > this.Count() - 1) throw new IndexOutOfRangeException();
                        
                    var j = 0;
                    var acc = this;

                    while (i != j) 
                    {
                        acc = acc.Rest;
                        j++;
                    }

                    acc.Data = value;
                }
                
            }

            /// <summary>
            /// Inserts an element at index, pushing back index behind the new node.
            /// </summary>
            public void Insert(int index, T value)
            {
                if (index > this.Count() - 1) throw new IndexOutOfRangeException();

                var j = 0;
                var acc = this;

                //get the index before where we want to insert
                while (index-1 != j)
                {
                    acc = acc.Rest;
                    j++;
                }
                
                //hold a ref to the list being pushed back
                var temp = acc.Rest;

                //insert node and append the rest of list
                acc.Rest = new Node<T>(value,temp);
            }

            /// <summary>
            /// Deletes the node at index
            /// </summary>
            public void Delete(int index)
            {
                if (index > this.Count() - 1) throw new IndexOutOfRangeException();

                var j = 0;
                var acc = this;

                //get node before our node
                while (index-1 != j)
                {
                    acc = acc.Rest;
                    j++;
                }
                
                //hold a ref to the list after node to be deleted 
                var temp = acc.Rest.Rest;

                //"Delete" the node. GC can handle the rest
                acc.Rest = temp;
            }

           #region overrides and Linq

            public override string ToString()
            {
                var sb = new StringBuilder("[");

                return this.Aggregate(sb, (builder, node) => builder.Append($"{node},"), builder => builder.Append(']').ToString());
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public IEnumerator<T> GetEnumerator()
            {
                var current = this;

                while (true)
                {
                    if(current.Rest is null) yield break;
                    yield return current.Data;
                    current = current.Rest;
                }

            }

            #endregion
        }

        /// <summary>
        /// Creates a list using the params
        /// </summary>
        /// <returns>The head of the list</returns>
        public static Node<T> MakeList<T>(params T[] args)
        {
            var result = new Node<T>();

            args.Aggregate(result, (list, data) =>
            {
                list.Data = data;
                list.Rest = new Node<T>();
                return list.Rest;
            });

            return result;
        }

        /// <summary>
        /// Eagerly consumes the IEnumerable and produces a list
        /// </summary>
        public static Node<T> MakeList<T>(IEnumerable<T> source)
        {
            var result = new Node<T>();

            source.Aggregate(result, (list, data) =>
            {
                list.Data = data;
                list.Rest = new Node<T>();
                return list.Rest;
            });

            return result;
        }
    } 


}
