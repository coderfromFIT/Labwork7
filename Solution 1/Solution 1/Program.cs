using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



// Generic Calculator class
public class Calculator<T>
{
    // Delegates for basic arithmetic operations
    public delegate T AdditionDelegate(T a, T b);
    public delegate T SubtractionDelegate(T a, T b);
    public delegate T MultiplicationDelegate(T a, T b);
    public delegate T DivisionDelegate(T a, T b);

    // Event to notify when an operation is performed
    public event Action<string> OperationPerformed;

    // Methods to perform arithmetic operations using delegates
    public T Add(T a, T b, AdditionDelegate additionDelegate)
    {
        T result = additionDelegate(a, b);
        OnOperationPerformed($"Addition: {a} + {b} = {result}");
        return result;
    }

    public T Subtract(T a, T b, SubtractionDelegate subtractionDelegate)
    {
        T result = subtractionDelegate(a, b);
        OnOperationPerformed($"Subtraction: {a} - {b} = {result}");
        return result;
    }

    public T Multiply(T a, T b, MultiplicationDelegate multiplicationDelegate)
    {
        T result = multiplicationDelegate(a, b);
        OnOperationPerformed($"Multiplication: {a} * {b} = {result}");
        return result;
    }

    public T Divide(T a, T b, DivisionDelegate divisionDelegate)
    {
        if (EqualityComparer<T>.Default.Equals(b, default(T)))
        {
            OnOperationPerformed("Cannot divide by zero.");
            return default(T);
        }

        T result = divisionDelegate(a, b);
        OnOperationPerformed($"Division: {a} / {b} = {result}");
        return result;
    }

    // Helper method to raise the OperationPerformed event
    private void OnOperationPerformed(string operation)
    {
        OperationPerformed?.Invoke(operation);
    }
}

// Example Usage
class Program
{
    static void Main()
    {
        // Create an instance of Calculator for int type
        Calculator<int> intCalculator = new Calculator<int>();

        // Define delegate instances for int type
        Calculator<int>.AdditionDelegate intAddition = (a, b) => a + b;
        Calculator<int>.SubtractionDelegate intSubtraction = (a, b) => a - b;
        Calculator<int>.MultiplicationDelegate intMultiplication = (a, b) => a * b;
        Calculator<int>.DivisionDelegate intDivision = (a, b) => a / b;

        // Perform operations for int type
        int resultAdd = intCalculator.Add(5, 3, intAddition);
        int resultSubtract = intCalculator.Subtract(8, 3, intSubtraction);
        int resultMultiply = intCalculator.Multiply(4, 6, intMultiplication);
        int resultDivide = intCalculator.Divide(10, 2, intDivision);

        // Display results
        Console.WriteLine($"Result of Addition: {resultAdd}");
        Console.WriteLine($"Result of Subtraction: {resultSubtract}");
        Console.WriteLine($"Result of Multiplication: {resultMultiply}");
        Console.WriteLine($"Result of Division: {resultDivide}");

        // Create an instance of Calculator for double type
        Calculator<double> doubleCalculator = new Calculator<double>();

        // Define delegate instances for double type
        Calculator<double>.AdditionDelegate doubleAddition = (a, b) => a + b;
        Calculator<double>.SubtractionDelegate doubleSubtraction = (a, b) => a - b;
        Calculator<double>.MultiplicationDelegate doubleMultiplication = (a, b) => a * b;
        Calculator<double>.DivisionDelegate doubleDivision = (a, b) => a / b;

        // Perform operations for double type
        double resultAddDouble = doubleCalculator.Add(5.5, 3.2, doubleAddition);
        double resultSubtractDouble = doubleCalculator.Subtract(8.7, 3.1, doubleSubtraction);
        double resultMultiplyDouble = doubleCalculator.Multiply(4.2, 6.3, doubleMultiplication);
        double resultDivideDouble = doubleCalculator.Divide(10.0, 2.5, doubleDivision);

        // Display results
        Console.WriteLine($"Result of Addition (Double): {resultAddDouble}");
        Console.WriteLine($"Result of Subtraction (Double): {resultSubtractDouble}");
        Console.WriteLine($"Result of Multiplication (Double): {resultMultiplyDouble}");
        Console.WriteLine($"Result of Division (Double): {resultDivideDouble}");
        Console.ReadKey();
    }
}
