using System;
using System.Collections.Generic;

namespace MTSTest
{
    static class Program
    {
        static void Main(string[] args)
        {
            var array = new[] {20, 16, 34, 39, 6};
            var result = Sort(array, 33, 39);
            foreach (var element in result)
            {
                Console.WriteLine(element);
            }
        }

        /// <summary>
        /// Возвращает отсортированный по возрастанию поток чисел
        /// </summary>
        /// <param name="inputStream">Поток чисел от 0 до maxValue. Длина потока не превышает миллиарда чисел.</param>
        /// <param name="sortFactor">Фактор упорядоченности потока. Неотрицательное число.
        /// Если в потоке встретилось число x, то в нём больше не встретятся числа меньше, чем (x - sortFactor).</param>
        /// <param name="maxValue">Максимально возможное значение чисел в потоке. Неотрицательное число, не превышающее 2000.</param>
        /// <returns>Отсортированный по возрастанию поток чисел.</returns>
        static IEnumerable<int> Sort(IEnumerable<int> inputStream, int sortFactor, int maxValue)
        {
            //TODO array -> use maxValue
            var values = new SortedList<int, int>();
            foreach (int x in inputStream)
            {
                //TODO use sortFactor
                if (!values.TryAdd(x, 1))
                {
                    values[x]++;
                }
            }

            foreach (var element in values)
            {
                int counter = 0;
                while (element.Value - counter > 0)
                {
                    yield return element.Key;
                    counter++;
                }
            }
        }

    }
}