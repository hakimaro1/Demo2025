using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1
            double y;
            Console.Write("Введите первое число: ");
            double x1 = double.Parse(Console.ReadLine());
            Console.Write("Введите второе число: ");
            double x2 = double.Parse(Console.ReadLine());

            double x = x1;
            do
            {
                y = Math.Sin(x);
                Console.WriteLine("{0:0.00}\t{1:0.00}", x, y);
                x += 0.01;
            }
            while (x <= x2);
            Console.WriteLine();
            // 2
            Console.Write("Введите первое число: ");
            int a = int.Parse(Console.ReadLine());
            Console.Write("Введите второе число: ");
            int b = int.Parse(Console.ReadLine());

            while (a != b)
            {
                if (a > b)
                    a -= b;
                else
                    b -= a;

                //  _ = a > b ? a -= b : b -= a;
                // о пустой переменной:
                // https://learn.microsoft.com/ru-ru/dotnet/csharp/fundamentals/functional/discards#a-standalone-discard
            }
            int nod = a;
            Console.WriteLine("nod = " + nod);
            Console.WriteLine();

            Console.Write("Введите целое число: ");
            string input = Console.ReadLine();

            if (!long.TryParse(input, out long number))
            {
                Console.WriteLine("Ошибка: введено не целое число.");
                return;
            }

            number = Math.Abs(number);  // Работаем с модулем числа
            int sum = 0;

            while (number > 0)
            {
             sum += (int)(number % 10);  // Последняя цифра
             number /= 10;               // Убираем последнюю цифру
            }

            Console.WriteLine($"Сумма цифр: {sum}");
            }
        }

    }


