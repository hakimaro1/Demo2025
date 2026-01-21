
using System;

class Distance
{
    public int feet;
    public double inches;

    // Конструктор по умолчанию
    public Distance()
    {
        this.feet = 0;
        this.inches = 0.0;
    }

    // Конструктор с параметрами
    public Distance(int feet, double inches)
    {
        this.feet = feet;
        this.inches = inches;
        Normalize();
    }

    private void Normalize()
    {
        if (inches >= 12.0)
        {
            int additionalFeet = (int)(inches / 12.0);
            feet += additionalFeet;
            inches = inches % 12.0;
        }
    }

    // Перегрузка оператора сложения
    public static Distance operator +(Distance d1, Distance d2)
    {
        double totalInches = d1.feet * 12.0 + d1.inches + d2.feet * 12.0 + d2.inches;
        int newFeet = (int)(totalInches / 12.0);
        double newInches = totalInches % 12.0;
        return new Distance(newFeet, newInches);
    }

    // Перегрузка оператора вычитания
    public static Distance operator -(Distance d1, Distance d2)
    {
        double totalInches1 = d1.feet * 12.0 + d1.inches;
        double totalInches2 = d2.feet * 12.0 + d2.inches;
        double resultInches = totalInches1 - totalInches2;

        if (resultInches < 0)
        {
            resultInches = 0;
        }

        int newFeet = (int)(resultInches / 12.0);
        double newInches = resultInches % 12.0;
        return new Distance(newFeet, newInches);
    }

    // Перегрузка оператора равенства
    public static bool operator ==(Distance d1, Distance d2)
    {
        if (ReferenceEquals(d1, d2))
            return true;
        if (ReferenceEquals(d1, null) || ReferenceEquals(d2, null))
            return false;

        double totalInches1 = d1.feet * 12.0 + d1.inches;
        double totalInches2 = d2.feet * 12.0 + d2.inches;
        return Math.Abs(totalInches1 - totalInches2) < 0.001;
    }

    // Перегрузка оператора неравенства
    public static bool operator !=(Distance d1, Distance d2)
    {
        return !(d1 == d2);
    }

    // Перегрузка оператора меньше
    public static bool operator <(Distance d1, Distance d2)
    {
        double totalInches1 = d1.feet * 12.0 + d1.inches;
        double totalInches2 = d2.feet * 12.0 + d2.inches;
        return totalInches1 < totalInches2;
    }

    // Перегрузка оператора больше
    public static bool operator >(Distance d1, Distance d2)
    {
        return d2 < d1;
    }

    // Перегрузка оператора меньше или равно
    public static bool operator <=(Distance d1, Distance d2)
    {
        return (d1 < d2) || (d1 == d2);
    }

    // Перегрузка оператора больше или равно
    public static bool operator >=(Distance d1, Distance d2)
    {
        return (d1 > d2) || (d1 == d2);
    }

    // Переопределение метода Equals
    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is Distance))
            return false;
        return this == (Distance)obj;
    }

    // Переопределение метода GetHashCode
    public override int GetHashCode()
    {
        return (feet * 12.0 + inches).GetHashCode();
    }

    // Переопределение метода ToString
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
        double inches1 = double.Parse(Console.ReadLine());

        Console.WriteLine("\nВведите значения для второго расстояния:");
        Console.Write("Футы: ");
        int feet2 = int.Parse(Console.ReadLine());
        Console.Write("Дюймы: ");
        double inches2 = double.Parse(Console.ReadLine());

        // Инициализация первых двух объектов
        Distance distance1 = new Distance(feet1, inches1);
        Distance distance2 = new Distance(feet2, inches2);

        // Третий объект - сумма первых двух
        Distance distance3 = distance1 + distance2;

        // Четвертый объект - разность первых двух
        Distance distance4 = distance1 - distance2;

        Console.WriteLine("\nРезультаты:");
        Console.WriteLine($"Первое расстояние: {distance1}");
        Console.WriteLine($"Второе расстояние: {distance2}");
        Console.WriteLine($"Сумма: {distance3}");
        Console.WriteLine($"Разность: {distance4}");

        // Тестирование операторов сравнения
        Console.WriteLine("\nСравнение расстояний:");
        Console.WriteLine($"distance1 == distance2: {distance1 == distance2}");
        Console.WriteLine($"distance1 != distance2: {distance1 != distance2}");
        Console.WriteLine($"distance1 < distance2: {distance1 < distance2}");
        Console.WriteLine($"distance1 > distance2: {distance1 > distance2}");
        Console.WriteLine($"distance1 <= distance2: {distance1 <= distance2}");
        Console.WriteLine($"distance1 >= distance2: {distance1 >= distance2}");

        Console.ReadKey();
    }
}