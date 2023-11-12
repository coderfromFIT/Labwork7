using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



// Generic FunctionCache class
public class FunctionCache<TKey, TResult>
{
    // Delegate for user-defined function
    public delegate TResult FuncDelegate(TKey key);

    // Dictionary to store cached results
    private Dictionary<TKey, CacheItem> cache;

    // Class to represent a cache item
    private class CacheItem
    {
        public TResult Result { get; set; }
        public DateTime ExpiryTime { get; set; }
    }

    // Constructor
    public FunctionCache()
    {
        cache = new Dictionary<TKey, CacheItem>();
    }

    // Method to execute a user-defined function and cache the result
    public TResult Execute(FuncDelegate function, TKey key, TimeSpan cacheDuration)
    {
        if (cache.ContainsKey(key) && DateTime.Now < cache[key].ExpiryTime)
        {
            // Return cached result if available and not expired
            Console.WriteLine($"Result retrieved from cache for key: {key}");
            return cache[key].Result;
        }

        // Execute the function if not found in the cache or expired
        TResult result = function(key);

        // Cache the result with expiry time
        cache[key] = new CacheItem { Result = result, ExpiryTime = DateTime.Now.Add(cacheDuration) };

        Console.WriteLine($"Result calculated and cached for key: {key}");

        return result;
    }
}

// Example Usage
class Program
{
    static void Main()
    {
        // Create an instance of FunctionCache for string key and int result
        FunctionCache<string, int> cache = new FunctionCache<string, int>();

        // Define a user-defined function
        FunctionCache<string, int>.FuncDelegate userFunction = key =>
        {
            Console.WriteLine($"Executing user-defined function for key: {key}");
            // Simulate some heavy computation
            System.Threading.Thread.Sleep(2000);
            return key.Length;
        };

        // Execute the function with caching
        int result1 = cache.Execute(userFunction, "hello", TimeSpan.FromSeconds(5));
        int result2 = cache.Execute(userFunction, "world", TimeSpan.FromSeconds(5));
        int result3 = cache.Execute(userFunction, "hello", TimeSpan.FromSeconds(5));

        // Display results
        Console.WriteLine($"Result 1: {result1}");
        Console.WriteLine($"Result 2: {result2}");
        Console.WriteLine($"Result 3: {result3}");
        Console.ReadKey();
    }
}
