using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

internal static class Program
{
    private static void Main()
    {
        CultureInfo culture = CultureInfo.InvariantCulture;

        // Ввод одного треугольника (опционально)
        Console.WriteLine("Введите длины сторон треугольника (через пробел):");
        string input = Console.ReadLine();


        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Не указаны длины сторон.");
            return;
        }

        string[] parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        double a, b, c;

        if (parts.Length != 3
            || !double.TryParse(parts[0], NumberStyles.Float, culture, out a)
            || !double.TryParse(parts[1], NumberStyles.Float, culture, out b)
            || !double.TryParse(parts[2], NumberStyles.Float, culture, out c))
        {
            Console.WriteLine("Некорректный ввод. Укажите три числа.");
            return;
        }

        TriangleLab6 triangle = new TriangleLab6(a, b, c);

        if (!triangle.IsValid())
        {
            Console.WriteLine("Треугольник с такими сторонами не существует.");
            return;
        }

        Console.WriteLine("Стороны треугольника:");
        triangle.PrintSides();
        Console.WriteLine("Периметр: {0:F3}", triangle.GetPerimeter());
        Console.WriteLine("Площадь: {0:F3}", triangle.GetArea());

        // Раздел сортировки
        Console.WriteLine("\n" + new string('-', 50));
        Console.WriteLine("СОРТИРОВКА ТРЕУГОЛЬНИКОВ ПО ПЛОЩАДИ");
        Console.WriteLine(new string('-', 50));

        List<TriangleLab6> triangles = new List<TriangleLab6>
        {
            new TriangleLab6(3, 4, 5),
            new TriangleLab6(5, 5, 5),
            new TriangleLab6(2, 3, 4),
            new TriangleLab6(6, 8, 10),
            new TriangleLab6(7, 7, 7),
            new TriangleLab6(1, 1, 1)
        };

        triangles.Sort();

        Console.WriteLine("Отсортированные треугольники (по возрастанию площади):");
        Console.WriteLine(new string('-', 60));

        int index = 1;
        foreach (TriangleLab6 tri in triangles)
        {
            Console.WriteLine($"{index}. Стороны: a={tri.A:F2}, b={tri.B:F2}, c={tri.C:F2}");
            Console.WriteLine($"   Площадь: {tri.GetArea():F2} кв.ед.");
            Console.WriteLine($"   Периметр: {tri.GetPerimeter():F2} ед.");
            Console.WriteLine(new string('-', 40));
            index++;
        }
    }
}
