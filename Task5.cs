using System;
using System.IO;

namespace MTSTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TransformToElephant();
            Console.WriteLine("Муха");
            
            //...
        }

        private static void TransformToElephant()
        {
            Console.WriteLine("Слон");
            Console.SetOut(TextWriter.Null);
        }
    }
}