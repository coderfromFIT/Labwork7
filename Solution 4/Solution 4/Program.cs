using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Generic TaskScheduler class
public class TaskScheduler<TTask, TPriority>
{
    // Delegate for task execution
    public delegate void TaskExecution(TTask task);

    // Class to represent a task with priority
    private class PriorityTask
    {
        public TTask Task { get; set; }
        public TPriority Priority { get; set; }
    }

    // Priority queue to store tasks
    private PriorityQueue<PriorityTask, TPriority> taskQueue;

    // Constructor
    public TaskScheduler()
    {
        taskQueue = new PriorityQueue<PriorityTask, TPriority>();
    }

    // Method to add a task with priority to the scheduler
    public void AddTask(TTask task, TPriority priority)
    {
        PriorityTask priorityTask = new PriorityTask { Task = task, Priority = priority };
        taskQueue.Enqueue(priorityTask, priority);
    }

    // Method to execute the task with the highest priority
    public void ExecuteNext(TaskExecution taskExecution)
    {
        if (taskQueue.Count > 0)
        {
            PriorityTask nextTask = taskQueue.Dequeue();
            taskExecution(nextTask.Task);
        }
        else
        {
            Console.WriteLine("No tasks to execute.");
        }
    }
}

// Generic PriorityQueue class
public class PriorityQueue<TItem, TPriority>
{
    private readonly SortedDictionary<TPriority, Queue<TItem>> priorityQueue;

    public PriorityQueue()
    {
        priorityQueue = new SortedDictionary<TPriority, Queue<TItem>>();
    }

    public void Enqueue(TItem item, TPriority priority)
    {
        if (!priorityQueue.ContainsKey(priority))
        {
            priorityQueue[priority] = new Queue<TItem>();
        }

        priorityQueue[priority].Enqueue(item);
    }

    public TItem Dequeue()
    {
        if (priorityQueue.Count == 0)
        {
            throw new InvalidOperationException("Queue is empty.");
        }

        var highestPriority = priorityQueue.Keys.Last();
        var itemQueue = priorityQueue[highestPriority];

        if (itemQueue.Count == 0)
        {
            throw new InvalidOperationException("Queue is empty.");
        }

        var item = itemQueue.Dequeue();

        if (itemQueue.Count == 0)
        {
            priorityQueue.Remove(highestPriority);
        }

        return item;
    }

    public int Count
    {
        get
        {
            return priorityQueue.Values.Sum(queue => queue.Count);
        }
    }
}

// Example Usage
class Program
{
    static void Main()
    {
        // Create an instance of TaskScheduler with string task and int priority
        TaskScheduler<string, int> taskScheduler = new TaskScheduler<string, int>();

        // Define a task execution delegate
        TaskScheduler<string, int>.TaskExecution taskExecution = task =>
        {
            Console.WriteLine($"Executing task: {task}");
        };

        // Add tasks with priorities
        taskScheduler.AddTask("Task 1", 3);
        taskScheduler.AddTask("Task 2", 1);
        taskScheduler.AddTask("Task 3", 2);

        // Execute tasks with highest priority
        taskScheduler.ExecuteNext(taskExecution);
        taskScheduler.ExecuteNext(taskExecution);
        taskScheduler.ExecuteNext(taskExecution);
        taskScheduler.ExecuteNext(taskExecution); // No tasks to execute

        Console.ReadKey();
    }
}
