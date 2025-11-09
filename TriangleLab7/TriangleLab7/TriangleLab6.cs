using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TriangleLab6 : IComparable<TriangleLab6>
{
    public double A { get; private set; }
    public double B { get; private set; }
    public double C { get; private set; }

    public TriangleLab6(double a, double b, double c)
    {
        SetSides(a, b, c);
    }

    public void SetSides(double a, double b, double c)
    {
        A = a;
        B = b;
        C = c;
    }

    public bool IsValid()
    {
        return A > 0 && B > 0 && C > 0
               && A + B > C
               && A + C > B
               && B + C > A;
    }

    public double GetPerimeter()
    {
        if (!IsValid())
            throw new InvalidOperationException("Треугольник с такими сторонами не существует.");
        return A + B + C;
    }

    public double GetArea()
    {
        if (!IsValid())
            throw new InvalidOperationException("Треугольник с такими сторонами не существует.");
        double s = GetPerimeter() / 2.0;
        return Math.Sqrt(s * (s - A) * (s - B) * (s - C));
    }

    public int CompareTo(TriangleLab6 other)
    {
        if (other == null) return 1;
        return GetArea().CompareTo(other.GetArea());
    }

    public void PrintSides()
    {
        Console.WriteLine("a = {0}, b = {1}, c = {2}", A, B, C);
    }
}
