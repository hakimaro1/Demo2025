using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadraticEquationSolver
{
  
    public static class QuadraticSolver
    {
     
        public static (int, double[]) Solve(double a, double b, double c)
        {
            // Проверяем, что a не равно нулю
            if (a == 0)
                throw new ArgumentException("Коэффициент a не может быть равен нулю (уравнение не квадратное)");

            // Вычисляем дискриминант
            double discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                // Нет действительных корней
                return (0, Array.Empty<double>());
            }
            else if (discriminant == 0)
            {
                // Один корень (два совпадающих)
                double x = -b / (2 * a);
                return (1, new double[] { x });
            }
            else
            {
                // Два различных корня
                double sqrtD = Math.Sqrt(discriminant);
                double x1 = (-b - sqrtD) / (2 * a);
                double x2 = (-b + sqrtD) / (2 * a);
                return (2, new double[] { x1, x2 });
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Решение квадратного уравнения ax² + bx + c = 0");
            Console.WriteLine("----------------------------------------");

            try
            {
                // Ввод коэффициентов
                Console.Write("Введите коэффициент a: ");
                double a = double.Parse(Console.ReadLine());

                Console.Write("Введите коэффициент b: ");
                double b = double.Parse(Console.ReadLine());

                Console.Write("Введите коэффициент c: ");
                double c = double.Parse(Console.ReadLine());

                // Решение уравнения
                var (rootCount, roots) = QuadraticSolver.Solve(a, b, c);

                // Вывод результата
                Console.WriteLine("\nРезультат:");
                Console.WriteLine($"Уравнение: {a}x² + {b}x + {c} = 0");

                if (rootCount == 0)
                {
                    Console.WriteLine("Действительных корней нет (дискриминант < 0)");
                }
                else if (rootCount == 1)
                {
                    Console.WriteLine($"Один корень: x = {roots[0]:F6}");
                }
                else // rootCount == 2
                {
                    Console.WriteLine($"Два корня: x₁ = {roots[0]:F6}, x₂ = {roots[1]:F6}");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: введено не числовое значение!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}