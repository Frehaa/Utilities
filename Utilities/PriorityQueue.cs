using System;
using System.Collections;
using System.Collections.Generic;

namespace Utilities
{   
    /** 
     * <summary>Min ordered Priority Queue</summary>
     * 
     */
    public class PriorityQueue<T> : ICollection<T>
    {
        public delegate int ComparerMethod(T left, T right);

        private static readonly int DEFAULT_INITIAL_SIZE = 2;
        private static ComparerMethod defaultComparer = (T left, T right) => Comparer.Default.Compare(left, right);
        
        private T[] heap;
        private int endIndex = 0;
        private ComparerMethod comparer;
        
        public int Count => endIndex;
        public bool IsEmpty => Count == 0;
        public bool IsReadOnly => false;
        
        public PriorityQueue(int initialSize, ComparerMethod comparer)
        {
            heap = new T[initialSize];
            this.comparer = comparer;
        }
        public PriorityQueue() : this(DEFAULT_INITIAL_SIZE) { }
        public PriorityQueue(int initialSize) : this(initialSize, defaultComparer) { }
        public PriorityQueue(ComparerMethod comparer) : this(DEFAULT_INITIAL_SIZE, comparer) { }

        public void Add(T item)
        {
            if (endIndex == heap.Length - 1)
                Resize(heap.Length * 2);

            ++endIndex;
            heap[endIndex] = item;
            Swim(endIndex);
        }

        public T RemoveMin()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Can't call RemoveMin when the PriorityQueue is empty");
                
            T tmp = heap[1];

            Swap(1, endIndex--);
            Sink(1);

            if (Count >= 2 && Count < heap.Length/2)
                Resize(heap.Length / 2);

            return tmp;
        }

        private void Resize(int v)
        {
            T[] tmp = new T[v];
            for (int i = 1; i < heap.Length && i < v; ++i)
                tmp[i] = heap[i];

            heap = tmp;
        }

        private void Swim(int k)
        {   
            while (k > 1 && Less(k, k / 2))
            {
                Swap(k, k / 2);
                k = k / 2;
            }
        }
        
        private void Sink(int k)
        {
            while (k * 2 <= Count)
            {
                int j = 2 * k;
                if (j < Count && Less(j + 1, j)) j++;
                if (Less(k, j)) break;
                Swap(k, j);
                k = j;
            }
        }

        private bool Less(int i, int j)
        {
            return comparer(heap[i], heap[j]) < 0;
        }

        private void Swap(int i, int j)
        {
            T tmp = heap[i];
            heap[i] = heap[j];
            heap[j] = tmp;
        }

        public void Clear()
        {
            heap = new T[DEFAULT_INITIAL_SIZE];
        }

        public bool Contains(T item)
        {
            foreach (T element in this)
            {
                if (comparer(element, item) == 0)
                    return true;
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("arrayIndex");
            if (array.Length - arrayIndex < Count)
                throw new ArgumentException("Not enough space for all elements in the array", "array, arrayIndex");

            int i = arrayIndex;
            foreach (T item in this)
            {
                array[i++] = item;
            }
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 1; i < endIndex; ++i)
            {
                yield return heap[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
