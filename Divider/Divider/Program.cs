namespace Divider
{
    internal class DivideIt
    {
        public static void Main(string[] args)
        {
            // Запрос первого числа
            Console.Write("Please enter the first integer: ");
            int i = int.Parse(Console.ReadLine());

            // Запрос второго числа
            Console.Write("Please enter the second integer: ");
            int j = int.Parse(Console.ReadLine());

            // Умножение
            int m = i * j;
            Console.WriteLine($"The result of multiplying {i} by {j} is {m}");

            // Деление (с проверкой на ноль)
            if (j != 0)
            {
                int k = i / j;
                Console.WriteLine($"The result of dividing {i} by {j} is {k}");
            }
            else
            {
                Console.WriteLine("Error: Division by zero is not allowed.");
            }
            Console.ReadLine();
        }
    }
}
