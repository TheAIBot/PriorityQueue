﻿using System;
using System.Collections.Generic;

namespace PriorityQueue
{
    public class PriorityQueue<T, TPriority>
    {
        private ValuePriority<T, TPriority>[] Heap = new ValuePriority<T, TPriority>[1];
        private readonly IComparer<TPriority> ValueComparar;
        public int Count { get; private set; } = 0;

        public PriorityQueue() : this(Comparer<TPriority>.Default)
        {
        }

        public PriorityQueue(IComparer<TPriority> comparer)
        {
            ValueComparar = comparer;
        }

        public void Enqueue(T t, TPriority priority)
        {
            if (Count == Heap.Length)
            {
                ExpandHeap();
            }

            Heap[Count] = new ValuePriority<T, TPriority>(t, priority);
            BubbleUp(Count);
            Count++;
        }

        public T Peek()
        {
            return PeekWithPriority().Value;
        }

        public TPriority PeekPriority()
        {
            return PeekWithPriority().Priority;
        }

        public ValuePriority<T, TPriority> PeekWithPriority()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The priority queue is empty");
            }

            return Heap[0];
        }

        public T Dequeue()
        {
            return DequeueWithPriority().Value;
        }

        public ValuePriority<T, TPriority> DequeueWithPriority()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The priority queue is empty");
            }

            var top = Heap[0];
            Heap[0] = Heap[Count - 1];
            Count--;
            BubbleDown(0);

            return top;
        }

        private void ExpandHeap()
        {
            Array.Resize(ref Heap, Count * 2 + 1);
        }

        private void BubbleUp(int childIndex)
        {
            while (!IsRoot(childIndex))
            {
                int parentIndex = Parent(childIndex);
                var child = Heap[childIndex];
                var parent = Heap[parentIndex];
                if (ValueComparar.Compare(child.Priority, parent.Priority) >= 0)
                {
                    break;
                }

                Heap[parentIndex] = child;
                Heap[childIndex] = parent;
                childIndex = parentIndex;
            }
        }

        private void BubbleDown(int parentIndex)
        {
            while (!IsLeaf(parentIndex, Count))
            {
                int leftChildIndex = LeftChild(parentIndex);
                int rightChildIndex = RightChild(parentIndex);
                if (leftChildIndex >= Count)
                {
                    break;
                }

                int bestChildIndex = leftChildIndex;
                //If both childs are valid then the best child
                //between the two will be chosen. Otherwise the
                //old valid child will be the best child.
                if (rightChildIndex < Count)
                {
                    var leftChild = Heap[leftChildIndex];
                    var rightChild = Heap[rightChildIndex];

                    if (ValueComparar.Compare(leftChild.Priority, rightChild.Priority) > 0)
                    {
                        bestChildIndex = rightChildIndex;
                    }
                }

                var bestChild = Heap[bestChildIndex];
                var parent = Heap[parentIndex];
                if (ValueComparar.Compare(parent.Priority, bestChild.Priority) <= 0)
                {
                    break;
                }

                Heap[parentIndex] = bestChild;
                Heap[bestChildIndex] = parent;
                parentIndex = bestChildIndex;
            }
        }

        private static int LeftChild(int index)
        {
            return (2 * index) + 1;
        }

        private static int RightChild(int index)
        {
            return (2 * index) + 2;
        }

        private static int Parent(int index)
        {
            return (index - 1) / 2;
        }

        private static bool IsLeaf(int index, int count)
        {
            return (index >= count / 2) && (index < count);
        }

        private static bool IsRoot(int index)
        {
            return index == 0;
        }
    }
}
