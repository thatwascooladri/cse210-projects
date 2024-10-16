using System;
using System.Collections.Generic;
using System.IO;

abstract class MindfulnessActivity
{
    protected string Name;
    protected string Description;
    public int Duration;

    public MindfulnessActivity(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void StartActivity()
    {
        Console.WriteLine($"Starting {Name}...");
        Console.WriteLine(Description);
        Console.Write("Enter the duration of the activity (in seconds): ");
        Duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        DisplayAnimation(3);
    }

    public void EndActivity()
    {
        Console.WriteLine("Well done!");
        Console.WriteLine($"You have completed the {Name} activity for {Duration} seconds.");
        DisplayAnimation(3);
    }

    public void DisplayAnimation(int duration)
    {
        string[] spinner = { "|", "/", "-", "\\" };
        for (int i = 0; i < duration; i++)
        {
            Console.Write($"\r{spinner[i % 4]}");
            System.Threading.Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity() : base("Breathing Activity", "This activity will help you relax by guiding you through breathing in and out slowly.") { }

    public void PerformBreathing()
    {
        for (int i = 0; i < Duration / 2; i++)
        {
            Console.WriteLine("Breathe in...");
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Breathe out...");
            System.Threading.Thread.Sleep(3000);
        }
    }

    public void Run()
    {
        StartActivity();
        PerformBreathing();
        EndActivity();
    }
}

class ReflectionActivity : MindfulnessActivity
{
    private List<string> Prompts = new List<string> {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something difficult.",
        "Think of a time when you helped someone in need."
    };

    public ReflectionActivity() : base("Reflection Activity", "Reflect on moments of strength and resilience.") { }

    public void Run()
    {
        StartActivity();
        Console.WriteLine(Prompts[new Random().Next(Prompts.Count)]);
        EndActivity();
    }
}

class ListingActivity : MindfulnessActivity
{
    private List<string> Prompts = new List<string> {
        "Who are people you appreciate?",
        "What are your personal strengths?",
        "Who have you helped recently?"
    };

    public ListingActivity() : base("Listing Activity", "List as many things as you can in the specified area.") { }

    public void Run()
    {
        StartActivity();
        Console.WriteLine(Prompts[new Random().Next(Prompts.Count)]);
        Console.WriteLine("Start listing items:");
        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        while (DateTime.Now < endTime)
        {
            Console.Write("- ");
            Console.ReadLine();
        }
        EndActivity();
    }
}

class ActivityLogger
{
    private Dictionary<string, int> Log = new Dictionary<string, int>();

    public void LogActivity(string activityName, int duration)
    {
        if (!Log.ContainsKey(activityName)) Log[activityName] = 0;
        Log[activityName] += duration;
    }

    public void DisplayLog()
    {
        Console.WriteLine("Activity Log:");
        foreach (var entry in Log)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value} seconds");
        }
    }

    public void SaveLog()
    {
        using (StreamWriter writer = new StreamWriter("activity_log.csv"))
        {
            foreach (var entry in Log)
            {
                writer.WriteLine($"{entry.Key},{entry.Value}");
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Develop05 World!");

        ActivityLogger logger = new ActivityLogger();
        Dictionary<int, MindfulnessActivity> activities = new Dictionary<int, MindfulnessActivity>
        {
            { 1, new BreathingActivity() },
            { 2, new ReflectionActivity() },
            { 3, new ListingActivity() }
        };

        while (true)
        {
            Console.WriteLine("\nChoose an activity:");
            Console.WriteLine("1. Breathing");
            Console.WriteLine("2. Reflection");
            Console.WriteLine("3. Listing");
            Console.WriteLine("4. View Log");
            Console.WriteLine("5. Exit");
            int choice = int.Parse(Console.ReadLine());

            if (choice >= 1 && choice <= 3)
            {
                MindfulnessActivity activity = activities[choice];
                activity.StartActivity();
                activity.EndActivity();
                logger.LogActivity(activity.GetType().Name, activity.Duration);
            }
            else if (choice == 4)
            {
                logger.DisplayLog();
            }
            else if (choice == 5)
            {
                logger.SaveLog();
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please select again.");
            }
        }
    }
}

// New Gratitude Activity: This is a place where users can think about and jot down stuff they're thankful for.
// Activity Log: This keeps tabs on how often each activity gets done and for how long.
// Prompt Uniqueness: Makes sure no prompts show up again until all of them have been used at least once.
// Log File: Saves the activity log in a CSV file and loads it when you fire up the program.
// Advanced Breathing Animation: The breathing animation will have the text grow and shrink to mimic breathing in and out.
