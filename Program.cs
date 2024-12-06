using System; //++++___njnnmn

public abstract class AbstractCalc
{
    protected string make = "Generic Calculator";

    public string GetMake() => make;

    public abstract double Add();
    public abstract double Sub();
    public abstract double Mul();
    public abstract double Div();
}

// Інтерфейс IAdvanced
public interface IAdvanced
{
    double Pow(); // метод для переведення км у милі
    double Log(); // метод для обчислення lg(a)
}

// Обчислювальний модуль стандартного режиму
public class OrdinaryCalc : AbstractCalc
{
    protected double num1;
    protected double num2;

    public OrdinaryCalc()
    {
        make = "Standard Calculator";
    }

    public double GetNum1() => num1;
    public void SetNum1(double value) => num1 = value;

    public double GetNum2() => num2;
    public void SetNum2(double value) => num2 = value;

    public override double Add() => num1 + num2;
    public override double Sub() => num1 - num2;
    public override double Mul() => num1 * num2;

    public override double Div()
    {
        if (num2 == 0)
            throw new DivideByZeroException("Division by zero is not allowed.");
        return num1 / num2;
    }
}

// Обчислювальний модуль інженерного режиму
public class AdvancedCalc : OrdinaryCalc, IAdvanced
{
    public AdvancedCalc()
    {
        make = "Advanced Calculator";
    }

    public double Pow()
    {
        if (num1 < 1 || num1 > 1000)
            throw new ArgumentOutOfRangeException("Input value for conversion must be between 1 and 1000.");
        return num1 * 0.621371; // Конвертація км у милі
    }

    public double Log()
    {
        if (num1 < 1 || num1 > 1000000)
            throw new ArgumentOutOfRangeException("The value of 'a' must be in the range [1, 1000000].");
        return Math.Log10(num1);
    }
}

// Головний метод
public class MainClass
{
    public static void Main(string[] args)
    {
        AbstractCalc standardCalc = new OrdinaryCalc();
        IAdvanced advancedCalc = new AdvancedCalc();

        while (true)
        {
            Console.WriteLine("Choose mode: 'standard' or 'engineer' (type 'exit' to quit): ");
            string mode = Console.ReadLine();

            if (mode == "exit") break;

            Console.WriteLine("Enter the first number: ");
            double num1 = double.Parse(Console.ReadLine());

            if (mode == "standard")
            {
                Console.WriteLine("Enter the second number: ");
                double num2 = double.Parse(Console.ReadLine());

                ((OrdinaryCalc)standardCalc).SetNum1(num1);
                ((OrdinaryCalc)standardCalc).SetNum2(num2);

                Console.WriteLine("Choose operation: '+', '-', '*', '/': ");
                string operation = Console.ReadLine();

                double result;
                switch (operation)
                {
                    case "+":
                        result = standardCalc.Add();
                        break;
                    case "-":
                        result = standardCalc.Sub();
                        break;
                    case "*":
                        result = standardCalc.Mul();
                        break;
                    case "/":
                        result = standardCalc.Div();
                        break;
                    default:
                        throw new InvalidOperationException("Invalid operation.");
                }
                Console.WriteLine($"Result: {result}");
            }
            else if (mode == "engineer")
            {
                ((AdvancedCalc)advancedCalc).SetNum1(num1);

                Console.WriteLine("Choose advanced operation: 'pow', 'log': ");
                string operation = Console.ReadLine();

                try
                {
                    double result;
                    if (operation == "pow")
                    {
                        result = advancedCalc.Pow();
                        Console.WriteLine($"{num1} km = {result} miles");
                    }
                    else if (operation == "log")
                    {
                        result = advancedCalc.Log();
                        Console.WriteLine($"lg({num1}) = {result}");
                    }
                    else
                    {
                        throw new InvalidOperationException("Invalid operation.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid mode selected.");
            }

            Console.WriteLine("------");
        }
    }
}