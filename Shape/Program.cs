using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Интерфейс для вращения фигуры
public interface IRotatable
{
    void Rotate(double angle);
}

// Базовый класс для всех фигур
public abstract class Shape
{
    public abstract double GetPerimeter();
    public abstract double GetArea();
}

// Класс окружности
public class Circle : Shape
{
    private double radius;

    public Circle(double radius)
    {
        if (radius <= 0)
            throw new ArgumentException("Радиус должен быть положительным числом.");
        this.radius = radius;
    }

    public double GetRadius() => radius;

    public override double GetPerimeter() => 2 * Math.PI * radius;
    public override double GetArea() => Math.PI * radius * radius;
}

// Класс квадрата
public class Square : Shape, IRotatable
{
    private double side;

    public Square(double side)
    {
        if (side <= 0)
            throw new ArgumentException("Сторона квадрата должна быть положительным числом.");
        this.side = side;
    }

    public double GetSide() => side;

    public override double GetPerimeter() => 4 * side;
    public override double GetArea() => side * side;

    public void Rotate(double angle)
    {
        Console.WriteLine($"Квадрат повёрнут на {angle:F1}° вокруг центра.");
    }
}

// Класс треугольника
public class Triangle : Shape, IRotatable
{
    private double a, b, c;

    public Triangle(double a, double b, double c)
    {
        if (a <= 0 || b <= 0 || c <= 0)
            throw new ArgumentException("Длины сторон треугольника должны быть положительными числами.");

        if (!IsValidTriangle(a, b, c))
            throw new ArgumentException("Треугольник с такими сторонами не существует (не выполняется неравенство треугольника).");

        this.a = a;
        this.b = b;
        this.c = c;
    }

    private bool IsValidTriangle(double a, double b, double c)
    {
        return a + b > c && a + c > b && b + c > a;
    }

    // Геттеры для доступа к сторонам треугольника
    public double GetA() => a;
    public double GetB() => b;
    public double GetC() => c;

    public override double GetPerimeter() => a + b + c;

    public override double GetArea()
    {
        double p = GetPerimeter() / 2;
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }

    public void Rotate(double angle)
    {
        Console.WriteLine($"Треугольник повёрнут на {angle:F1}° вокруг центра.");
    }
}

// Основной класс программы
namespace Lab9Class
{
    class Program
    {
        static void Main()
        {
            Circle circle = null;
            Square square = null;
            Triangle triangle = null;

            // Ввод радиуса окружности
            while (circle == null)
            {
                Console.Write("Введите радиус окружности: ");
                string input = Console.ReadLine();

                if (double.TryParse(input, out double radius) && radius > 0)
                {
                    try
                    {
                        circle = new Circle(radius);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}. Попробуйте ещё раз.");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка: радиус должен быть положительным числом. Попробуйте ещё раз.");
                }
            }

            // Ввод стороны квадрата
            while (square == null)
            {
                Console.Write("Введите сторону квадрата: ");
                string input = Console.ReadLine();

                if (double.TryParse(input, out double side) && side > 0)
                {
                    try
                    {
                        square = new Square(side);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}. Попробуйте ещё раз.");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка: сторона квадрата должна быть положительным числом. Попробуйте ещё раз.");
                }
            }

            // Ввод сторон треугольника
            while (triangle == null)
            {
                Console.WriteLine("Введите длины сторон треугольника (a, b, c):");

                Console.Write("a = ");
                string inputA = Console.ReadLine();
                Console.Write("b = ");
                string inputB = Console.ReadLine();
                Console.Write("c = ");
                string inputC = Console.ReadLine();

                if (double.TryParse(inputA, out double a) &&
                    double.TryParse(inputB, out double b) &&
                    double.TryParse(inputC, out double c) &&
                    a > 0 && b > 0 && c > 0)
                {
                    try
                    {
                        triangle = new Triangle(a, b, c);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}. Попробуйте ещё раз.");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка: все стороны должны быть положительными числами. Попробуйте ещё раз.");
                }
            }

            // Выводим информацию о фигурах
            Console.WriteLine("\n=== Результаты расчётов ===");
            Console.WriteLine($"Окружность: радиус = {circle.GetRadius():F2}");
            Console.WriteLine($"Периметр (длина окружности) = {circle.GetPerimeter():F2}");
            Console.WriteLine($"Площадь = {circle.GetArea():F2}");

            Console.WriteLine($"\nКвадрат: сторона = {square.GetSide():F2}");
            Console.WriteLine($"Периметр = {square.GetPerimeter():F2}");
            Console.WriteLine($"Площадь = {square.GetArea():F2}");

            Console.WriteLine($"\nТреугольник: стороны a={triangle.GetA():F2}, b={triangle.GetB():F2}, c={triangle.GetC():F2}");
            Console.WriteLine($"Периметр = {triangle.GetPerimeter():F2}");
            Console.WriteLine($"Площадь = {triangle.GetArea():F2}");

            // Вращаем фигуры
            Console.Write("\nВведите угол поворота квадрата (в градусах): ");
            if (double.TryParse(Console.ReadLine(), out double angleSquare))
            {
                square.Rotate(angleSquare);
            }
            else
            {
                Console.WriteLine("Некорректный ввод угла. Поворот квадрата пропущен.");
            }

            Console.Write("Введите угол поворота треугольника (в градусах): ");
            if (double.TryParse(Console.ReadLine(), out double angleTriangle))
            {
                triangle.Rotate(angleTriangle);
            }
            else
            {
                Console.WriteLine("Некорректный ввод угла. Поворот треугольника пропущен.");
            }

            Console.WriteLine("\nПрограмма завершена.");
        }
    }
}
