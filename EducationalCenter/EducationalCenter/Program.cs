using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCenter
{
 
    public abstract class Person
    {
    
        public string Surname { get; protected set; }


        public DateTime DateOfBirth { get; protected set; }


        protected Person(string surname, DateTime dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentException("Фамилия не может быть пустой", nameof(surname));

            if (dateOfBirth > DateTime.Now)
                throw new ArgumentException("Дата рождения не может быть в будущем", nameof(dateOfBirth));

            Surname = surname;
            DateOfBirth = dateOfBirth;
        }

        public int CalculateAge()
        {
            var today = DateTime.Today;
            var age = today.Year - DateOfBirth.Year;

  
            if (DateOfBirth.Date > today.AddYears(-age))
                age--;

            return age;
        }


        public abstract string GetInfo();

        public override string ToString()
        {
            return GetInfo();
        }
    }

    public interface IEmployee
    {
     
        decimal CalculateSalary();

        int CalculateWorkExperience();

        string GetPosition();

        string GetEmployeeInfo();
    }

    public class Administrator : Person, IEmployee
    {
        public string Laboratory { get; protected set; }

        public DateTime StartDate { get; protected set; }

        public decimal BaseSalary { get; set; }

        public Administrator(string surname, DateTime dateOfBirth, string laboratory,
            DateTime startDate, decimal baseSalary = 50000)
            : base(surname, dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(laboratory))
                throw new ArgumentException("Лаборатория не может быть пустой", nameof(laboratory));

            if (startDate > DateTime.Now)
                throw new ArgumentException("Дата начала работы не может быть в будущем", nameof(startDate));

            Laboratory = laboratory;
            StartDate = startDate;
            BaseSalary = baseSalary;
        }

        public decimal CalculateSalary()
        {
            int experience = CalculateWorkExperience();
         
            decimal experienceBonus = BaseSalary * 0.05m * Math.Min(experience, 10);
            return BaseSalary + experienceBonus;
        }

        public int CalculateWorkExperience()
        {
            var today = DateTime.Today;
            var years = today.Year - StartDate.Year;

            if (StartDate.Date > today.AddYears(-years))
                years--;

            return Math.Max(0, years);
        }

        public string GetPosition()
        {
            return "Администратор";
        }

        public string GetEmployeeInfo()
        {
            return $"Должность: {GetPosition()}, Стаж: {CalculateWorkExperience()} лет, " +
                   $"Зарплата: {CalculateSalary():C}, Лаборатория: {Laboratory}";
        }

        public override string GetInfo()
        {
            return $"=== АДМИНИСТРАТОР ===\n" +
                   $"Фамилия: {Surname}\n" +
                   $"Дата рождения: {DateOfBirth:dd.MM.yyyy}\n" +
                   $"Возраст: {CalculateAge()} лет\n" +
                   $"Лаборатория: {Laboratory}\n" +
                   $"Дата начала работы: {StartDate:dd.MM.yyyy}\n" +
                   $"Стаж работы: {CalculateWorkExperience()} лет\n" +
                   $"Заработная плата: {CalculateSalary():C}\n";
        }
    }

    public class Student : Person
    {
        public string Faculty { get; protected set; }
        public string Group { get; protected set; }
        public int Course { get; set; }
        public double AverageGrade { get; set; }
        public Student(string surname, DateTime dateOfBirth, string faculty,
            string group, int course = 1, double averageGrade = 0.0)
            : base(surname, dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(faculty))
                throw new ArgumentException("Факультет не может быть пустым", nameof(faculty));

            if (string.IsNullOrWhiteSpace(group))
                throw new ArgumentException("Группа не может быть пустой", nameof(group));

            if (course < 1 || course > 6)
                throw new ArgumentException("Курс должен быть от 1 до 6", nameof(course));

            if (averageGrade < 0 || averageGrade > 5)
                throw new ArgumentException("Средний балл должен быть от 0 до 5", nameof(averageGrade));

            Faculty = faculty;
            Group = group;
            Course = course;
            AverageGrade = averageGrade;
        }

        public bool IsExcellentStudent()
        {
            return AverageGrade >= 4.5;
        }

        public override string GetInfo()
        {
            string excellentStatus = IsExcellentStudent() ? " (Отличник)" : "";
            return $"=== СТУДЕНТ ===\n" +
                   $"Фамилия: {Surname}\n" +
                   $"Дата рождения: {DateOfBirth:dd.MM.yyyy}\n" +
                   $"Возраст: {CalculateAge()} лет\n" +
                   $"Факультет: {Faculty}\n" +
                   $"Группа: {Group}\n" +
                   $"Курс: {Course}\n" +
                   $"Средний балл: {AverageGrade:F2}{excellentStatus}\n";
        }
    }

    public class Teacher : Person, IEmployee
    {
 
        public string Faculty { get; protected set; }

        public string Position { get; protected set; }

        public DateTime StartDate { get; protected set; }

        public decimal BaseSalary { get; set; }

        public int HoursPerWeek { get; set; }

        public Teacher(string surname, DateTime dateOfBirth, string faculty,
            string position, DateTime startDate, decimal baseSalary = 60000, int hoursPerWeek = 18)
            : base(surname, dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(faculty))
                throw new ArgumentException("Факультет не может быть пустым", nameof(faculty));

            if (string.IsNullOrWhiteSpace(position))
                throw new ArgumentException("Должность не может быть пустой", nameof(position));

            if (startDate > DateTime.Now)
                throw new ArgumentException("Дата начала работы не может быть в будущем", nameof(startDate));

            Faculty = faculty;
            Position = position;
            StartDate = startDate;
            BaseSalary = baseSalary;
            HoursPerWeek = hoursPerWeek;
        }

  
        public decimal CalculateSalary()
        {
            int experience = CalculateWorkExperience();
            decimal experienceBonus = BaseSalary * 0.03m * Math.Min(experience, 10);

            decimal overtimeBonus = 0;
            if (HoursPerWeek > 18)
            {
                overtimeBonus = BaseSalary * 0.1m * (HoursPerWeek - 18) / 18;
            }

            return BaseSalary + experienceBonus + overtimeBonus;
        }

        public int CalculateWorkExperience()
        {
            var today = DateTime.Today;
            var years = today.Year - StartDate.Year;

            if (StartDate.Date > today.AddYears(-years))
                years--;

            return Math.Max(0, years);
        }

  
        public string GetPosition()
        {
            return Position;
        }


        public string GetEmployeeInfo()
        {
            return $"Должность: {GetPosition()}, Стаж: {CalculateWorkExperience()} лет, " +
                   $"Зарплата: {CalculateSalary():C}, Факультет: {Faculty}, " +
                   $"Часов в неделю: {HoursPerWeek}";
        }

  
        public override string GetInfo()
        {
            return $"=== ПРЕПОДАВАТЕЛЬ ===\n" +
                   $"Фамилия: {Surname}\n" +
                   $"Дата рождения: {DateOfBirth:dd.MM.yyyy}\n" +
                   $"Возраст: {CalculateAge()} лет\n" +
                   $"Факультет: {Faculty}\n" +
                   $"Должность: {Position}\n" +
                   $"Дата начала работы: {StartDate:dd.MM.yyyy}\n" +
                   $"Стаж работы: {CalculateWorkExperience()} лет\n" +
                   $"Часов в неделю: {HoursPerWeek}\n" +
                   $"Заработная плата: {CalculateSalary():C}\n";
        }
    }


    public class Manager : Person, IEmployee
    {
   
        public string Faculty { get; protected set; }

    
        public string Position { get; protected set; }

   
        public DateTime StartDate { get; protected set; }

        public decimal BaseSalary { get; set; }

        public int SubordinatesCount { get; set; }

        public Manager(string surname, DateTime dateOfBirth, string faculty,
            string position, DateTime startDate, decimal baseSalary = 70000, int subordinatesCount = 0)
            : base(surname, dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(faculty))
                throw new ArgumentException("Факультет не может быть пустым", nameof(faculty));

            if (string.IsNullOrWhiteSpace(position))
                throw new ArgumentException("Должность не может быть пустой", nameof(position));

            if (startDate > DateTime.Now)
                throw new ArgumentException("Дата начала работы не может быть в будущем", nameof(startDate));

            Faculty = faculty;
            Position = position;
            StartDate = startDate;
            BaseSalary = baseSalary;
            SubordinatesCount = subordinatesCount;
        }

        public decimal CalculateSalary()
        {
            int experience = CalculateWorkExperience();
       
            decimal experienceBonus = BaseSalary * 0.04m * Math.Min(experience, 10);

            decimal managementBonus = BaseSalary * 0.01m * Math.Min(SubordinatesCount, 20);

            return BaseSalary + experienceBonus + managementBonus;
        }

        public int CalculateWorkExperience()
        {
            var today = DateTime.Today;
            var years = today.Year - StartDate.Year;

            if (StartDate.Date > today.AddYears(-years))
                years--;

            return Math.Max(0, years);
        }

  
        public string GetPosition()
        {
            return Position;
        }

        public string GetEmployeeInfo()
        {
            return $"Должность: {GetPosition()}, Стаж: {CalculateWorkExperience()} лет, " +
                   $"Зарплата: {CalculateSalary():C}, Факультет: {Faculty}, " +
                   $"Подчиненных: {SubordinatesCount}";
        }

        public override string GetInfo()
        {
            return $"=== МЕНЕДЖЕР ===\n" +
                   $"Фамилия: {Surname}\n" +
                   $"Дата рождения: {DateOfBirth:dd.MM.yyyy}\n" +
                   $"Возраст: {CalculateAge()} лет\n" +
                   $"Факультет: {Faculty}\n" +
                   $"Должность: {Position}\n" +
                   $"Дата начала работы: {StartDate:dd.MM.yyyy}\n" +
                   $"Стаж работы: {CalculateWorkExperience()} лет\n" +
                   $"Количество подчиненных: {SubordinatesCount}\n" +
                   $"Заработная плата: {CalculateSalary():C}\n";
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== УЧЕБНЫЙ ЦЕНТР ===\n");

            List<Person> persons = new List<Person>();

            persons.Add(new Administrator(
                "Иванов",
                new DateTime(1985, 5, 15),
                "Лаборатория информационных технологий",
                new DateTime(2010, 9, 1),
                55000
            ));

            persons.Add(new Administrator(
                "Петрова",
                new DateTime(1990, 8, 22),
                "Лаборатория физики",
                new DateTime(2015, 3, 15),
                52000
            ));

            persons.Add(new Student(
                "Сидоров",
                new DateTime(2003, 3, 10),
                "Информационные технологии",
                "ИТ-21-1",
                2,
                4.8
            ));

            persons.Add(new Student(
                "Козлова",
                new DateTime(2004, 7, 5),
                "Экономика",
                "ЭК-22-2",
                1,
                4.2
            ));

            persons.Add(new Student(
                "Морозов",
                new DateTime(2002, 11, 18),
                "Информационные технологии",
                "ИТ-20-1",
                3,
                3.9
            ));

            persons.Add(new Teacher(
                "Волков",
                new DateTime(1978, 2, 14),
                "Информационные технологии",
                "Доцент",
                new DateTime(2005, 9, 1),
                65000,
                20
            ));

            persons.Add(new Teacher(
                "Новикова",
                new DateTime(1982, 9, 30),
                "Экономика",
                "Профессор",
                new DateTime(2008, 2, 1),
                75000,
                18
            ));

            persons.Add(new Teacher(
                "Лебедев",
                new DateTime(1990, 12, 5),
                "Информационные технологии",
                "Старший преподаватель",
                new DateTime(2016, 9, 1),
                60000,
                16
            ));

            persons.Add(new Manager(
                "Соколова",
                new DateTime(1980, 4, 20),
                "Информационные технологии",
                "Заведующий кафедрой",
                new DateTime(2012, 1, 15),
                80000,
                15
            ));

            persons.Add(new Manager(
                "Орлов",
                new DateTime(1975, 6, 8),
                "Экономика",
                "Декан факультета",
                new DateTime(2003, 9, 1),
                90000,
                25
            ));

            Console.WriteLine("=== ПОЛНАЯ ИНФОРМАЦИЯ О ВСЕХ ПЕРСОНАХ ===\n");
            foreach (var person in persons)
            {
                Console.WriteLine(person.GetInfo());
                Console.WriteLine(new string('-', 50));
                Console.WriteLine();
            }

            Console.WriteLine("\n=== СТАТИСТИКА ===\n");
            Console.WriteLine($"Всего персон: {persons.Count}");
            Console.WriteLine($"Администраторов: {persons.OfType<Administrator>().Count()}");
            Console.WriteLine($"Студентов: {persons.OfType<Student>().Count()}");
            Console.WriteLine($"Преподавателей: {persons.OfType<Teacher>().Count()}");
            Console.WriteLine($"Менеджеров: {persons.OfType<Manager>().Count()}");

            Console.WriteLine("\n=== ИНФОРМАЦИЯ О СОТРУДНИКАХ ===\n");
            var employees = persons.OfType<IEmployee>();
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.GetEmployeeInfo()}");
            }

            decimal totalSalary = employees.Sum(e => e.CalculateSalary());
            Console.WriteLine($"\nОбщая сумма заработных плат: {totalSalary:C}");

            double averageAge = persons.Average(p => p.CalculateAge());
            Console.WriteLine($"Средний возраст: {averageAge:F1} лет");

            var excellentStudents = persons.OfType<Student>().Where(s => s.IsExcellentStudent());
            Console.WriteLine($"\nСтудентов-отличников: {excellentStudents.Count()}");
            if (excellentStudents.Any())
            {
                Console.WriteLine("Отличники:");
                foreach (var student in excellentStudents)
                {
                    Console.WriteLine($"  - {student.Surname} (Группа: {student.Group}, Балл: {student.AverageGrade:F2})");
                }
            }

            var employeesWithExperience = employees
                .OrderByDescending(e => e.CalculateWorkExperience())
                .Take(3);
            Console.WriteLine("\n=== ТОП-3 СОТРУДНИКА ПО СТАЖУ ===\n");
            foreach (var emp in employeesWithExperience)
            {
                var person = emp as Person;
                Console.WriteLine($"{person.Surname}: {emp.CalculateWorkExperience()} лет стажа");
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}