using System;
using System.Globalization;

namespace MTSTest
{
    class Program
    {
        static readonly IFormatProvider _ifp = CultureInfo.InvariantCulture;

        class Number
        {
            readonly int _number;

            public Number(int number)
            {
                _number = number;
            }

            public override string ToString()
            {
                return _number.ToString(_ifp);
            }

            public static string operator +(Number n1, string n2)
            {
                var success = int.TryParse(n2, NumberStyles.Integer, _ifp, out var number2);
                if (!success) throw new InvalidCastException($"Can not convert '{nameof(n2)}' to int");
				
                var result = n1._number + number2;
                return result.ToString();
            }
        }

        static void Main(string[] args)
        {
            int someValue1 = 10;
            int someValue2 = 5;

            string result = new Number(someValue1) + someValue2.ToString(_ifp);
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}