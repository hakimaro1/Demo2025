using System;

struct Distance
{
    public int feet;
    public int inches;

    public Distance(int feet, int inches)
    {
        this.feet = feet;
        this.inches = inches;
        Normalize();
    }

    private void Normalize()
    {
        if (inches >= 12)
        {
            feet += inches / 12;
            inches = inches % 12;
        }
    }

    public override string ToString()
    {
        return $"{feet}'- {inches}\"";
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите значения для первого расстояния:");
        Console.Write("Футы: ");
        int feet1 = int.Parse(Console.ReadLine());
        Console.Write("Дюймы: ");
        int inches1 = int.Parse(Console.ReadLine());

        Console.WriteLine("\nВведите значения для второго расстояния:");
        Console.Write("Футы: ");
        int feet2 = int.Parse(Console.ReadLine());
        Console.Write("Дюймы: ");
        int inches2 = int.Parse(Console.ReadLine());

        Distance distance1 = new Distance(feet1, inches1);
        Distance distance2 = new Distance(feet2, inches2);
        Distance distance3;

        Distance Z;
        Z.inches = distance1.feet * 12 + distance1.inches + distance2.feet * 12 + distance2.inches;
        Z.feet = (int)(Z.inches / 12);
        Z.inches = Z.inches % 12;
        distance3 = Z;

        Console.WriteLine("\nРезультаты:");
        Console.WriteLine($"Первое расстояние: {distance1}");
        Console.WriteLine($"Второе расстояние: {distance2}");
        Console.WriteLine($"Сумма: {distance3}");

        Console.ReadKey();
    }
}
