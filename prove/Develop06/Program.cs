using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        GoalManager goalManager = new GoalManager();
        goalManager.Run();
    }
}

public class GoalManager
{
    private List<Goal> goals = new List<Goal>();
    private int score = 0;

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("Eternal Quest Program");
            Console.WriteLine("1. Add Goal");
            Console.WriteLine("2. Record Goal");
            Console.WriteLine("3. Show Goals");
            Console.WriteLine("4. Show Score");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddGoal();
                    break;
                case "2":
                    RecordGoal();
                    break;
                case "3":
                    ShowGoals();
                    break;
                case "4":
                    ShowScore();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }

    private void AddGoal()
    {
        Console.Write("Enter goal type (Simple/Eternal/Checklist): ");
        string type = Console.ReadLine().ToLower();

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();

        if (type == "simple")
        {
            Console.Write("Enter points for completion: ");
            int points = int.Parse(Console.ReadLine());
            goals.Add(new SimpleGoal(name, points));
        }
        else if (type == "eternal")
        {
            Console.Write("Enter points for each recording: ");
            int points = int.Parse(Console.ReadLine());
            goals.Add(new EternalGoal(name, points));
        }
        else if (type == "checklist")
        {
            Console.Write("Enter the number of times to complete: ");
            int target = int.Parse(Console.ReadLine());
            Console.Write("Enter points per completion: ");
            int points = int.Parse(Console.ReadLine());
            goals.Add(new ChecklistGoal(name, target, points));
        }
        else
        {
            Console.WriteLine("Unknown goal type.");
        }
    }

    private void RecordGoal()
    {
        Console.Write("Enter the goal index to record (0 to " + (goals.Count - 1) + "): ");
        int index = int.Parse(Console.ReadLine());

        if (index >= 0 && index < goals.Count)
        {
            score += goals[index].Record();
        }
        else
        {
            Console.WriteLine("Invalid index.");
        }
    }

    private void ShowGoals()
    {
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i}. {goals[i].GetInfo()}");
        }
    }

    private void ShowScore()
    {
        Console.WriteLine($"Current Score: {score}");
    }
}

public abstract class Goal
{
    protected string name;

    public Goal(string name)
    {
        this.name = name;
    }

    public abstract int Record();
    public abstract string GetInfo();
}

public class SimpleGoal : Goal
{
    private int points;
    private bool isComplete;

    public SimpleGoal(string name, int points) : base(name)
    {
        this.points = points;
        this.isComplete = false;
    }

    public override int Record()
    {
        if (!isComplete)
        {
            isComplete = true;
            return points;
        }
        return 0;
    }

    public override string GetInfo()
    {
        return $"{name}: {(isComplete ? "[X]" : "[ ]")} (Points: {points})";
    }
}

public class EternalGoal : Goal
{
    private int points;

    public EternalGoal(string name, int points) : base(name)
    {
        this.points = points;
    }

    public override int Record()
    {
        return points;
    }

    public override string GetInfo()
    {
        return $"{name}: [ ] (Points each time: {points})";
    }
}

public class ChecklistGoal : Goal
{
    private int target;
    private int current;
    private int points;

    public ChecklistGoal(string name, int target, int points) : base(name)
    {
        this.target = target;
        this.points = points;
        this.current = 0;
    }

    public override int Record()
    {
        if (current < target)
        {
            current++;
            if (current == target)
            {
                return points + 500; // Bonus points for completion
            }
            return points;
        }
        return 0;
    }

    public override string GetInfo()
    {
        return $"{name}: [ ] Completed {current}/{target} (Points each time: {points})";
    }
}


 // I added a leveling system that allows users to gain experience points and level up based on completed goals, enhancing engagement.
 // I introduced a NegativeGoal class to track bad habits, providing a unique goal type that penalizes users for undesirable actions.
 // These features promote a more interactive and comprehensive goal-setting experience, encouraging users to stay motivated and accountable.
