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
            var values = new int[maxValue + 1];
            var min = 0;
            foreach (var x in inputStream)
            {
                values[x]++;

                while (x - sortFactor > min)
                {
                    while (values[min] > 0)
                    {
                        values[min]--;
                        yield return min;
                    }
                    min++;
                }
            }

            while (min < values.Length)
            {
                while (values[min] > 0)
                {
                    values[min]--;
                    yield return min;
                }
                min++;
            }
        }

    }
}