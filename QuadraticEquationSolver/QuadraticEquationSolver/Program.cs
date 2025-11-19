using System;

namespace QuadraticEquationSolver
{
    public static class QuadraticSolver
    {
        public static (int rootCount, double? x1, double? x2) Solve(double a, double b, double c)
        {
            if (a == 0)
                throw new ArgumentException("Коэффициент a не может быть равен нулю (уравнение не квадратное)");

            double discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                return (0, null, null);
            }
            else if (discriminant == 0)
            {
                double x = -b / (2 * a);
                return (1, x, null);
            }
            else
            {
                double sqrtD = Math.Sqrt(discriminant);
                double x1 = (-b - sqrtD) / (2 * a);
                double x2 = (-b + sqrtD) / (2 * a);
                return (2, x1, x2);
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
                Console.Write("Введите коэффициент a: ");
                double a = double.Parse(Console.ReadLine());

                Console.Write("Введите коэффициент b: ");
                double b = double.Parse(Console.ReadLine());

                Console.Write("Введите коэффициент c: ");
                double c = double.Parse(Console.ReadLine());

                var (rootCount, x1, x2) = QuadraticSolver.Solve(a, b, c);

                Console.WriteLine("\nРезультат:");
                Console.WriteLine($"Уравнение: {a}x² + {b}x + {c} = 0");

                if (rootCount == 0)
                {
                    Console.WriteLine("Действительных корней нет (дискриминант < 0)");
                }
                else if (rootCount == 1)
                {
                    Console.WriteLine($"Один корень: x = {x1:F6}");
                }
                else
                {
                    Console.WriteLine($"Два корня: x₁ = {x1:F6}, x₂ = {x2:F6}");
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
