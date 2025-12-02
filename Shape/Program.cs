using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Структура для позиции фигуры
public struct Point
{
    public double X { get; set; }
    public double Y { get; set; }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    public override string ToString() => $"({X:F2}, {Y:F2})";
}

// Перечисление для цвета фигуры
public enum ShapeColor
{
    Красный,
    Синий,
    Зеленый,
    Желтый,
    Фиолетовый,
    Оранжевый,
    Черный,
    Белый
}

// Интерфейс для вращения фигуры
public interface IRotatable
{
    void Rotate(double angle);
    double RotationAngle { get; set; }
}

// Интерфейс для масштабирования фигуры
public interface IScalable
{
    void Scale(double factor);
    double ScaleFactor { get; set; }
}

// Базовый класс для всех фигур с общим поведением и свойствами
public abstract class Shape
{
    protected Shape(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Color = ShapeColor.Черный;
        Position = new Point(0, 0);
        IsVisible = true;
        CreatedDate = DateTime.Now;
    }

    // Основные свойства фигуры
    public string Name { get; protected set; }
    public ShapeColor Color { get; set; }
    public Point Position { get; set; }
    public bool IsVisible { get; set; }
    public DateTime CreatedDate { get; }

    public abstract double GetPerimeter();
    public abstract double GetArea();

    // Общее поведение: печать информации о фигуре
    public virtual void PrintInfo()
    {
        Console.WriteLine($"\n{Name}:");
        Console.WriteLine($"  Цвет: {Color}");
        Console.WriteLine($"  Позиция: {Position}");
        Console.WriteLine($"  Видимость: {(IsVisible ? "Видима" : "Скрыта")}");
        Console.WriteLine($"  Периметр = {GetPerimeter():F2}");
        Console.WriteLine($"  Площадь = {GetArea():F2}");
    }

    // Виртуальный метод для получения дополнительной информации
    public virtual string GetDetails() => $"{Name} - Периметр: {GetPerimeter():F2}, Площадь: {GetArea():F2}";
}

// Базовый класс для круглых фигур
public abstract class RoundShape : Shape
{
    protected RoundShape(string name, double radius) : base(name)
    {
        if (radius <= 0)
            throw new ArgumentException("Радиус должен быть положительным числом.");
        Radius = radius;
    }

    public double Radius { get; set; }

    protected virtual bool ValidateRadius(double value) => value > 0;
}

// Класс окружности
public class Circle : RoundShape, IScalable
{
    public Circle(double radius) : base("Окружность", radius)
    {
        ScaleFactor = 1.0;
    }

    public override double GetPerimeter() => 2 * Math.PI * Radius;
    public override double GetArea() => Math.PI * Radius * Radius;

    public double ScaleFactor { get; set; }

    public void Scale(double factor)
    {
        if (factor <= 0)
            throw new ArgumentException("Коэффициент масштабирования должен быть положительным.");
        Radius *= factor;
        ScaleFactor *= factor;
        Console.WriteLine($"Окружность масштабирована. Новый радиус: {Radius:F2}");
    }

    public override void PrintInfo()
    {
        Console.WriteLine($"\n{Name}: радиус = {Radius:F2}");
        base.PrintInfo();
    }
}

// Класс эллипса
public class Ellipse : RoundShape, IRotatable, IScalable
{
    private double secondRadius;

    public Ellipse(double radiusX, double radiusY) : base("Эллипс", radiusX)
    {
        if (radiusY <= 0)
            throw new ArgumentException("Второй радиус должен быть положительным числом.");
        SecondRadius = radiusY;
        RotationAngle = 0;
        ScaleFactor = 1.0;
    }

    public double SecondRadius
    {
        get => secondRadius;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Второй радиус должен быть положительным числом.");
            secondRadius = value;
        }
    }

    public double RotationAngle { get; set; }

    public double ScaleFactor { get; set; }

    public override double GetPerimeter()
    {
        // Приближенная формула Рамануджана для периметра эллипса
        double a = Math.Max(Radius, SecondRadius);
        double b = Math.Min(Radius, SecondRadius);
        double h = Math.Pow((a - b) / (a + b), 2);
        return Math.PI * (a + b) * (1 + 3 * h / (10 + Math.Sqrt(4 - 3 * h)));
    }

    public override double GetArea() => Math.PI * Radius * SecondRadius;

    public void Rotate(double angle)
    {
        RotationAngle = (RotationAngle + angle) % 360;
        Console.WriteLine($"Эллипс повёрнут на {angle:F1}°. Общий угол поворота: {RotationAngle:F1}°");
    }

    public void Scale(double factor)
    {
        if (factor <= 0)
            throw new ArgumentException("Коэффициент масштабирования должен быть положительным.");
        Radius *= factor;
        SecondRadius *= factor;
        ScaleFactor *= factor;
        Console.WriteLine($"Эллипс масштабирован. Новые радиусы: {Radius:F2}, {SecondRadius:F2}");
    }

    public override void PrintInfo()
    {
        Console.WriteLine($"\n{Name}: радиусы = {Radius:F2}, {SecondRadius:F2}");
        Console.WriteLine($"  Угол поворота: {RotationAngle:F1}°");
        base.PrintInfo();
    }
}

// Базовый класс для многоугольников
public abstract class Polygon : Shape
{
    protected Polygon(string name) : base(name)
    {
        NumberOfSides = 0;
    }

    public int NumberOfSides { get; protected set; }
}

// Класс квадрата
public class Square : Polygon, IRotatable, IScalable
{
    private double side;

    public Square(double side) : base("Квадрат")
    {
        if (side <= 0)
            throw new ArgumentException("Сторона квадрата должна быть положительным числом.");
        Side = side;
        NumberOfSides = 4;
        RotationAngle = 0;
        ScaleFactor = 1.0;
    }

    public double Side
    {
        get => side;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Сторона квадрата должна быть положительным числом.");
            side = value;
        }
    }

    public double RotationAngle { get; set; }
    public double ScaleFactor { get; set; }

    public override double GetPerimeter() => 4 * Side;
    public override double GetArea() => Side * Side;

    public override void PrintInfo()
    {
        Console.WriteLine($"\n{Name}: сторона = {Side:F2}");
        Console.WriteLine($"  Количество сторон: {NumberOfSides}");
        base.PrintInfo();
    }

    public void Rotate(double angle)
    {
        RotationAngle = (RotationAngle + angle) % 360;
        Console.WriteLine($"Квадрат повёрнут на {angle:F1}° вокруг центра. Общий угол: {RotationAngle:F1}°");
    }

    public void Scale(double factor)
    {
        if (factor <= 0)
            throw new ArgumentException("Коэффициент масштабирования должен быть положительным.");
        Side *= factor;
        ScaleFactor *= factor;
        Console.WriteLine($"Квадрат масштабирован. Новая сторона: {Side:F2}");
    }
}

// Класс прямоугольника
public class Rectangle : Polygon, IRotatable, IScalable
{
    private double width;
    private double height;

    public Rectangle(double width, double height) : base("Прямоугольник")
    {
        if (width <= 0)
            throw new ArgumentException("Ширина должна быть положительным числом.");
        if (height <= 0)
            throw new ArgumentException("Высота должна быть положительным числом.");
        Width = width;
        Height = height;
        NumberOfSides = 4;
        RotationAngle = 0;
        ScaleFactor = 1.0;
    }

    public double Width
    {
        get => width;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Ширина должна быть положительным числом.");
            width = value;
        }
    }

    public double Height
    {
        get => height;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Высота должна быть положительным числом.");
            height = value;
        }
    }

    public double RotationAngle { get; set; }
    public double ScaleFactor { get; set; }

    public override double GetPerimeter() => 2 * (Width + Height);
    public override double GetArea() => Width * Height;

    public void Rotate(double angle)
    {
        RotationAngle = (RotationAngle + angle) % 360;
        Console.WriteLine($"Прямоугольник повёрнут на {angle:F1}° вокруг центра. Общий угол: {RotationAngle:F1}°");
    }

    public void Scale(double factor)
    {
        if (factor <= 0)
            throw new ArgumentException("Коэффициент масштабирования должен быть положительным.");
        Width *= factor;
        Height *= factor;
        ScaleFactor *= factor;
        Console.WriteLine($"Прямоугольник масштабирован. Новые размеры: {Width:F2} x {Height:F2}");
    }

    public override void PrintInfo()
    {
        Console.WriteLine($"\n{Name}: ширина = {Width:F2}, высота = {Height:F2}");
        Console.WriteLine($"  Количество сторон: {NumberOfSides}");
        base.PrintInfo();
    }
}

// Класс ромба
public class Rhombus : Polygon, IRotatable, IScalable
{
    private double side;
    private double angle;

    public Rhombus(double side, double angleDegrees) : base("Ромб")
    {
        if (side <= 0)
            throw new ArgumentException("Сторона ромба должна быть положительным числом.");
        if (angleDegrees <= 0 || angleDegrees >= 180)
            throw new ArgumentException("Угол должен быть в диапазоне (0, 180) градусов.");
        Side = side;
        AngleDegrees = angleDegrees;
        NumberOfSides = 4;
        RotationAngle = 0;
        ScaleFactor = 1.0;
    }

    public double Side
    {
        get => side;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Сторона ромба должна быть положительным числом.");
            side = value;
        }
    }

    public double AngleDegrees
    {
        get => angle;
        set
        {
            if (value <= 0 || value >= 180)
                throw new ArgumentException("Угол должен быть в диапазоне (0, 180) градусов.");
            angle = value;
        }
    }

    public double RotationAngle { get; set; }
    public double ScaleFactor { get; set; }

    public override double GetPerimeter() => 4 * Side;

    public override double GetArea()
    {
        double angleRadians = AngleDegrees * Math.PI / 180;
        return Side * Side * Math.Sin(angleRadians);
    }

    public void Rotate(double angle)
    {
        RotationAngle = (RotationAngle + angle) % 360;
        Console.WriteLine($"Ромб повёрнут на {angle:F1}° вокруг центра. Общий угол: {RotationAngle:F1}°");
    }

    public void Scale(double factor)
    {
        if (factor <= 0)
            throw new ArgumentException("Коэффициент масштабирования должен быть положительным.");
        Side *= factor;
        ScaleFactor *= factor;
        Console.WriteLine($"Ромб масштабирован. Новая сторона: {Side:F2}");
    }

    public override void PrintInfo()
    {
        Console.WriteLine($"\n{Name}: сторона = {Side:F2}, угол = {AngleDegrees:F1}°");
        Console.WriteLine($"  Количество сторон: {NumberOfSides}");
        base.PrintInfo();
    }
}

// Класс треугольника
public class Triangle : Polygon, IRotatable, IScalable
{
    private double a, b, c;

    public Triangle(double a, double b, double c) : base("Треугольник")
    {
        if (a <= 0 || b <= 0 || c <= 0)
            throw new ArgumentException("Длины сторон треугольника должны быть положительными числами.");

        if (!IsValidTriangle(a, b, c))
            throw new ArgumentException("Треугольник с такими сторонами не существует (не выполняется неравенство треугольника).");

        SideA = a;
        SideB = b;
        SideC = c;
        NumberOfSides = 3;
        RotationAngle = 0;
        ScaleFactor = 1.0;
    }

    private bool IsValidTriangle(double a, double b, double c)
    {
        return a + b > c && a + c > b && b + c > a;
    }

    // Свойства для доступа к сторонам треугольника
    public double SideA
    {
        get => a;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Сторона должна быть положительным числом.");
            if (!IsValidTriangle(value, b, c))
                throw new ArgumentException("Треугольник с такими сторонами не существует.");
            a = value;
        }
    }

    public double SideB
    {
        get => b;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Сторона должна быть положительным числом.");
            if (!IsValidTriangle(a, value, c))
                throw new ArgumentException("Треугольник с такими сторонами не существует.");
            b = value;
        }
    }

    public double SideC
    {
        get => c;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Сторона должна быть положительным числом.");
            if (!IsValidTriangle(a, b, value))
                throw new ArgumentException("Треугольник с такими сторонами не существует.");
            c = value;
        }
    }

    public double RotationAngle { get; set; }
    public double ScaleFactor { get; set; }

    public override double GetPerimeter() => SideA + SideB + SideC;

    public override double GetArea()
    {
        double p = GetPerimeter() / 2;
        return Math.Sqrt(p * (p - SideA) * (p - SideB) * (p - SideC));
    }

    public override void PrintInfo()
    {
        Console.WriteLine($"\n{Name}: стороны a={SideA:F2}, b={SideB:F2}, c={SideC:F2}");
        Console.WriteLine($"  Количество сторон: {NumberOfSides}");
        base.PrintInfo();
    }

    public void Rotate(double angle)
    {
        RotationAngle = (RotationAngle + angle) % 360;
        Console.WriteLine($"Треугольник повёрнут на {angle:F1}° вокруг центра. Общий угол: {RotationAngle:F1}°");
    }

    public void Scale(double factor)
    {
        if (factor <= 0)
            throw new ArgumentException("Коэффициент масштабирования должен быть положительным.");
        SideA *= factor;
        SideB *= factor;
        SideC *= factor;
        ScaleFactor *= factor;
        Console.WriteLine($"Треугольник масштабирован. Новые стороны: {SideA:F2}, {SideB:F2}, {SideC:F2}");
    }
}

// Основной класс программы
namespace Lab9Class
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Программа для работы с иерархией фигур ===\n");

            // Список всех фигур
            List<Shape> shapes = new List<Shape>();

            // 1. Создание окружности
            Circle circle = CreateCircle();
            circle.Color = ShapeColor.Красный;
            circle.Position = new Point(10, 10);
            shapes.Add(circle);

            // 2. Создание квадрата
            Square square = CreateSquare();
            square.Color = ShapeColor.Синий;
            square.Position = new Point(20, 20);
            shapes.Add(square);

            // 3. Создание треугольника
            Triangle triangle = CreateTriangle();
            triangle.Color = ShapeColor.Зеленый;
            triangle.Position = new Point(30, 30);
            shapes.Add(triangle);

            // 4. Создание эллипса
            Console.WriteLine("\n--- Создание эллипса ---");
            Ellipse ellipse = CreateEllipse();
            ellipse.Color = ShapeColor.Фиолетовый;
            ellipse.Position = new Point(40, 40);
            shapes.Add(ellipse);

            // 5. Создание прямоугольника
            Console.WriteLine("\n--- Создание прямоугольника ---");
            Rectangle rectangle = CreateRectangle();
            rectangle.Color = ShapeColor.Оранжевый;
            rectangle.Position = new Point(50, 50);
            shapes.Add(rectangle);

            // 6. Создание ромба
            Console.WriteLine("\n--- Создание ромба ---");
            Rhombus rhombus = CreateRhombus();
            rhombus.Color = ShapeColor.Желтый;
            rhombus.Position = new Point(60, 60);
            shapes.Add(rhombus);

            // Выводим информацию о всех фигурах
            Console.WriteLine("\n\n=== Информация о всех фигурах ===");
            foreach (var shape in shapes)
            {
                shape.PrintInfo();
            }

            // Демонстрация работы с интерфейсами
            Console.WriteLine("\n\n=== Демонстрация операций с фигурами ===");

            // Поворот фигур
            Console.WriteLine("\n--- Поворот фигур ---");
            if (square is IRotatable rotSquare)
            {
                rotSquare.Rotate(45);
            }
            if (triangle is IRotatable rotTriangle)
            {
                rotTriangle.Rotate(30);
            }
            if (ellipse is IRotatable rotEllipse)
            {
                rotEllipse.Rotate(60);
            }

            // Масштабирование фигур
            Console.WriteLine("\n--- Масштабирование фигур ---");
            if (circle is IScalable scaleCircle)
            {
                scaleCircle.Scale(1.5);
            }
            if (square is IScalable scaleSquare)
            {
                scaleSquare.Scale(2.0);
            }
            if (ellipse is IScalable scaleEllipse)
            {
                scaleEllipse.Scale(0.75);
            }

            // Изменение свойств
            Console.WriteLine("\n--- Изменение свойств фигур ---");
            circle.IsVisible = false;
            Console.WriteLine($"Окружность теперь {(circle.IsVisible ? "видима" : "скрыта")}");
            circle.IsVisible = true;

            rectangle.Color = ShapeColor.Черный;
            Console.WriteLine($"Прямоугольник теперь имеет цвет: {rectangle.Color}");

            // Демонстрация работы со списком фигур
            Console.WriteLine("\n\n=== Статистика по фигурам ===");
            Console.WriteLine($"Всего фигур: {shapes.Count}");
            Console.WriteLine($"Полигонов: {shapes.OfType<Polygon>().Count()}");
            Console.WriteLine($"Круглых фигур: {shapes.OfType<RoundShape>().Count()}");
            Console.WriteLine($"Поворачиваемых фигур: {shapes.OfType<IRotatable>().Count()}");
            Console.WriteLine($"Масштабируемых фигур: {shapes.OfType<IScalable>().Count()}");

            double totalArea = shapes.Sum(s => s.GetArea());
            double totalPerimeter = shapes.Sum(s => s.GetPerimeter());
            Console.WriteLine($"\nОбщая площадь всех фигур: {totalArea:F2}");
            Console.WriteLine($"Общий периметр всех фигур: {totalPerimeter:F2}");

            // Вывод информации о фигурах после изменений
            Console.WriteLine("\n\n=== Фигуры после изменений ===");
            circle.PrintInfo();
            square.PrintInfo();
            ellipse.PrintInfo();

            Console.WriteLine("\nПрограмма завершена.");
            Console.ReadKey();
        }

        static Circle CreateCircle()
        {
            Circle circle = null;
            Console.WriteLine("--- Создание окружности ---");
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
            return circle;
        }

        static Square CreateSquare()
        {
            Square square = null;
            Console.WriteLine("\n--- Создание квадрата ---");
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
            return square;
        }

        static Triangle CreateTriangle()
        {
            Triangle triangle = null;
            Console.WriteLine("\n--- Создание треугольника ---");
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
            return triangle;
        }

        static Ellipse CreateEllipse()
        {
            Ellipse ellipse = null;
            while (ellipse == null)
            {
                Console.Write("Введите первый радиус эллипса: ");
                string inputX = Console.ReadLine();
                Console.Write("Введите второй радиус эллипса: ");
                string inputY = Console.ReadLine();

                if (double.TryParse(inputX, out double rx) && rx > 0 &&
                    double.TryParse(inputY, out double ry) && ry > 0)
                {
                    try
                    {
                        ellipse = new Ellipse(rx, ry);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}. Попробуйте ещё раз.");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка: радиусы должны быть положительными числами. Попробуйте ещё раз.");
                }
            }
            return ellipse;
        }

        static Rectangle CreateRectangle()
        {
            Rectangle rectangle = null;
            while (rectangle == null)
            {
                Console.Write("Введите ширину прямоугольника: ");
                string inputW = Console.ReadLine();
                Console.Write("Введите высоту прямоугольника: ");
                string inputH = Console.ReadLine();

                if (double.TryParse(inputW, out double width) && width > 0 &&
                    double.TryParse(inputH, out double height) && height > 0)
                {
                    try
                    {
                        rectangle = new Rectangle(width, height);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}. Попробуйте ещё раз.");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка: размеры должны быть положительными числами. Попробуйте ещё раз.");
                }
            }
            return rectangle;
        }

        static Rhombus CreateRhombus()
        {
            Rhombus rhombus = null;
            while (rhombus == null)
            {
                Console.Write("Введите сторону ромба: ");
                string inputS = Console.ReadLine();
                Console.Write("Введите угол ромба (в градусах, от 0 до 180): ");
                string inputA = Console.ReadLine();

                if (double.TryParse(inputS, out double side) && side > 0 &&
                    double.TryParse(inputA, out double angle) && angle > 0 && angle < 180)
                {
                    try
                    {
                        rhombus = new Rhombus(side, angle);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}. Попробуйте ещё раз.");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка: сторона должна быть положительным числом, угол - в диапазоне (0, 180). Попробуйте ещё раз.");
                }
            }
            return rhombus;
        }
    }
}