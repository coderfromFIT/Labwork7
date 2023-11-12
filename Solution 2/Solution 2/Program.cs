using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



// Generic Repository class
public class Repository<T>
{
    // Delegate for criteria
    public delegate bool Criteria<T>(T item);

    // List to store elements
    private List<T> elements;

    // Constructor
    public Repository()
    {
        elements = new List<T>();
    }

    // Method to add an element to the repository
    public void Add(T element)
    {
        elements.Add(element);
    }

    // Method to find elements based on criteria
    public List<T> Find(Criteria<T> criteria)
    {
        List<T> result = new List<T>();

        foreach (var element in elements)
        {
            if (criteria(element))
            {
                result.Add(element);
            }
        }

        return result;
    }
}

// Example Usage
class Program
{
    static void Main()
    {
        // Create an instance of Repository for int type
        Repository<int> intRepository = new Repository<int>();

        // Add elements to the repository
        intRepository.Add(10);
        intRepository.Add(20);
        intRepository.Add(30);
        intRepository.Add(40);
        intRepository.Add(50);

        // Define criteria for int type (find even numbers)
        Repository<int>.Criteria<int> evenNumberCriteria = x => x % 2 == 0;

        // Find elements based on criteria
        List<int> evenNumbers = intRepository.Find(evenNumberCriteria);

        // Display results
        Console.WriteLine("Even Numbers:");
        foreach (var number in evenNumbers)
        {
            Console.WriteLine(number);
        }

        // Create an instance of Repository for string type
        Repository<string> stringRepository = new Repository<string>();

        // Add elements to the repository
        stringRepository.Add("apple");
        stringRepository.Add("banana");
        stringRepository.Add("cherry");
        stringRepository.Add("date");
        stringRepository.Add("fig");

        // Define criteria for string type (find fruits starting with 'b')
        Repository<string>.Criteria<string> startsWithBCriteria = s => s.StartsWith("b", StringComparison.OrdinalIgnoreCase);

        // Find elements based on criteria
        List<string> bStartingFruits = stringRepository.Find(startsWithBCriteria);

        // Display results
        Console.WriteLine("\nFruits Starting with 'b':");
        foreach (var fruit in bStartingFruits)
        {
            Console.WriteLine(fruit);
        }
        Console.ReadKey();
    }
}

