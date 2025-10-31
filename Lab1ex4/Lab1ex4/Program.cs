using System;

namespace Divider
{
    class DivideIt
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Please enter the first integer");
            int i = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Please enter the second integer");
            int j = Int32.Parse(Console.ReadLine());

            int k = i / j;
            int m = i * j;

            Console.WriteLine($"The result of dividing {i} by {j} is {k}");
            Console.WriteLine($"The result of multiplying {i} by {j} is {m}");
        }
    }
}