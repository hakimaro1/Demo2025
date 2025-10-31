using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите периметр равностороннего треугольника: ");
        double perimeter = double.Parse(Console.ReadLine());

        if (perimeter <= 0)
        {
            Console.WriteLine("Ошибка: периметр должен быть положительным числом.");
            return;
        }
        // Расчет стороны равностороннего треугольника
        double side = perimeter / 3.0;

        // Расчет полупериметра
        double semiperimeter = perimeter / 2.0;

        // Расчет площади по формуле Герона
        // S = sqrt(p * (p - a) * (p - b) * (p - c))
        // Для равностороннего треугольника: a = b = c
        double area = Math.Sqrt(semiperimeter * (semiperimeter - side) *
                                 (semiperimeter - side) * (semiperimeter - side));

        // Вывод результатов в виде таблицы
        Console.WriteLine();
        Console.WriteLine("+---------------------------+");
        Console.WriteLine("+   Сторона   |   Площадь   +");
        Console.WriteLine("+                           +");
        Console.WriteLine($"+ {side,11:F2} | {area,11:F2} +");
        Console.WriteLine("+---------------------------+");

        Console.ReadLine();
    }
}