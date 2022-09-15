using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MTSTest
{
    static class Program
    {
        static void Main(string[] args)
        {
            var array = new[] {1, 2, 3, 4}.EnumerateFromTail(200);
            foreach (var tuple in array)
            {
                Console.WriteLine(tuple);
            }
        }

        /// <summary>
        /// <para> Отсчитать несколько элементов с конца </para>
        /// <example> new[] {1,2,3,4}.EnumerateFromTail(2) = (1, ), (2, ), (3, 1), (4, 0)</example>
        /// </summary> 
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="tailLength">Сколько элеметнов отсчитать с конца  (у последнего элемента tail = 0)</param>
        /// <returns></returns>
        public static IEnumerable<(T item, int? tail)> EnumerateFromTail<T>(
            this IEnumerable<T> enumerable, int? tailLength = null)
        {
            if (enumerable == null)
            {
                throw new ArgumentException($"'{nameof(enumerable)}' should not contain null value");
            }

            if (tailLength is null)
            {
                return enumerable.EnumerateFromTailWithoutTailLength();
            }
            return enumerable.EnumerateFromTailWithTailLength(tailLength.Value);
        }

        private static IEnumerable<(T item, int? tail)> EnumerateFromTailWithoutTailLength<T>(
            this IEnumerable<T> enumerable)
        {
            var length = enumerable.Count();
            foreach (var element in enumerable)
            {
                yield return (element, --length);
            }
        }

        private static IEnumerable<(T item, int? tail)> EnumerateFromTailWithTailLength<T>(
            this IEnumerable<T> enumerable, int tailLength)
        {
            //Prefer if tailLength >> enumerable.Count
            if (enumerable is ICollection<T> collection)
            {
                tailLength = Math.Min(collection.Count, tailLength);
            }
            
            if (tailLength <= 0)
            {
                yield break;
            }
            
            var queue = new PoppingQueue<T>(tailLength);
            foreach (var element in enumerable)
            {
                var poppedElement = queue.EnqueueAndPop(element);
                if (poppedElement != null)
                {
                    yield return (poppedElement.Value.Value, null);
                }
            }

            var counter = queue.Length;
            foreach (var element in queue)
            {
                yield return (element, --counter);
            }
        }

        public class PoppingQueue<T>:IEnumerable<T>
        {
            private readonly Queue<T> _queue;
            private readonly int _maxCapacity;

            public int Length => _queue.Count;
            
            public PoppingQueue(int maxCapacity)
            {
                _maxCapacity = maxCapacity;
                _queue = new Queue<T>(maxCapacity);
            }

            public QueueElement<T>? EnqueueAndPop(T element)
            {
                QueueElement<T>? poppedValue = null;
                if (_queue.Count >= _maxCapacity)
                {
                    poppedValue = new QueueElement<T>(_queue.Dequeue());
                }
                _queue.Enqueue(element);
                return poppedValue;
            }

            public IEnumerator<T> GetEnumerator()
            {
                return _queue.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public struct QueueElement<T>
        {
            public T Value { get; }
            public QueueElement(T value)
            {
                Value = value;
            }
        }
    }
}