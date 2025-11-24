using System;

public class ArithmeticProgression
{
    private readonly double firstElement;
    private readonly int requiredIndex;

    public ArithmeticProgression(double firstElement, int requiredIndex)
    {
        if (requiredIndex < 1)
            throw new ArgumentException("Номер требуемого элемента должен быть больше или равен 1.", nameof(requiredIndex));

        this.firstElement = firstElement;
        this.requiredIndex = requiredIndex;
    }

    public double CalculateRequiredElement(double difference)
    {
        return firstElement + (requiredIndex - 1) * difference;
    }

    public double FirstElement => firstElement;
    public int RequiredIndex => requiredIndex;
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Расчёт требуемого элемента арифметической прогрессии");
        Console.WriteLine("Формула: an = a1 + (n - 1) · d");

        try
        {
            Console.Write("Введите первый элемент: ");
            double a1 = ReadDouble();

            Console.Write("Введите номер требуемого элемента больше или равно 1: ");
            int n = ReadInt();

            var progression = new ArithmeticProgression(a1, n);

            Console.Write("Введите разность прогрессии: ");
            double d = ReadDouble();

            double result = progression.CalculateRequiredElement(d);
            Console.WriteLine($"Элемент a{n} = {result:F1}");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ошибка: {ex.Message}");
            Console.ResetColor();
        }

        Console.WriteLine("Программа завершена.");
    }

    private static double ReadDouble()
    {
        while (true)
        {
            if (double.TryParse(Console.ReadLine(), out double result))
                return result;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Некорректный ввод. Введите число (например, 2.5):");
            Console.ResetColor();
        }
    }

    private static int ReadInt()
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int result))
                return result;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Некорректный ввод. Введите целое число больше или равно 1:");
            Console.ResetColor();
        }
    }
}
