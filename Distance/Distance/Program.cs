using System;

class Program
{
    static void Main()
    {
        // Ввод первого расстояния
        Console.WriteLine("Введите первое расстояние:");
        Console.Write("Футы: ");
        int feet1 = int.Parse(Console.ReadLine());
        Console.Write("Дюймы: ");
        int inches1 = int.Parse(Console.ReadLine());

        // Ввод второго расстояния
        Console.WriteLine("\nВведите второе расстояние:");
        Console.Write("Футы: ");
        int feet2 = int.Parse(Console.ReadLine());
        Console.Write("Дюймы: ");
        int inches2 = int.Parse(Console.ReadLine());

        // Переводим всё в дюймы, складываем, затем обратно в футы и дюймы
        int totalInches = (feet1 * 12 + inches1) + (feet2 * 12 + inches2);
        int sumFeet = totalInches / 12;
        int sumInches = totalInches % 12;

        // Выводим результаты
        Console.WriteLine("\nРезультаты:");
        Console.WriteLine($"Первое расстояние: {feet1}'–{inches1}\"");
        Console.WriteLine($"Второе расстояние: {feet2}'–{inches2}\"");
        Console.WriteLine($"Сумма: {sumFeet}'–{sumInches}\"");

        Console.ReadKey(); // Чтобы программа не закрылась сразу
    }
}
