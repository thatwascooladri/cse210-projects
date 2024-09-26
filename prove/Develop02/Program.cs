using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class JournalProgramApp
{
    static void Main(string[] args)
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("Choose a program to run:");
            Console.WriteLine("1. Journal Program");
            Console.WriteLine("2. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    JournalProgram.Run();
                    break;
                case "2":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                    break;
            }
        }
    }
}

class JournalProgram
{
    public static void Run()
    {
        Journal journal = new Journal();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nJournal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Quick Notes Mode"); // New Feature: Quick Notes Mode
            Console.WriteLine("3. Display journal");
            Console.WriteLine("4. Save journal as CSV");
            Console.WriteLine("5. Save journal as JSON");
            Console.WriteLine("6. Load journal from JSON");
            Console.WriteLine("7. Exit");
            Console.Write("Choose an option (1-7): ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    journal.WriteNewEntry();
                    break;
                case "2":
                    journal.QuickNotesMode(); // Calls new Quick Notes Mode
                    break;
                case "3":
                    journal.DisplayEntries();
                    break;
                case "4":
                    Console.Write("Enter the filename to save the journal as CSV: ");
                    string saveCSVFile = Console.ReadLine();
                    journal.SaveToCSV(saveCSVFile); // Saves to CSV with proper handling
                    break;
                case "5":
                    Console.Write("Enter the filename to save the journal as JSON: ");
                    string saveJsonFile = Console.ReadLine();
                    journal.SaveToJson(saveJsonFile); // Saves to JSON format
                    break;
                case "6":
                    Console.Write("Enter the filename to load the journal from JSON: ");
                    string loadJsonFile = Console.ReadLine();
                    journal.LoadFromJson(loadJsonFile); // Loads from JSON format
                    break;
                case "7":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please enter 1-7.");
                    break;
            }
        }
    }
}

class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }
    public string EmotionTag { get; set; } // New field for emotion tag
    public string Weather { get; set; }    // New field for weather data
    public string Location { get; set; }   // New field for location

    public Entry(string prompt, string response, string emotionTag = "", string weather = "", string location = "")
    {
        Prompt = prompt;
        Response = response;
        Date = DateTime.Now.ToString("yyyy-MM-dd");
        EmotionTag = emotionTag;
        Weather = weather;
        Location = location;
    }

    public override string ToString()
    {
        return $"[{Date}] {Prompt}\nResponse: {Response}\nEmotion: {EmotionTag}\nWeather: {Weather}\nLocation: {Location}\n";
    }
}

class Journal
{
    private List<Entry> entries;
    private List<string> prompts;

    public Journal()
    {
        entries = new List<Entry>();
        prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "What is something I learned today?",
            "What am I most grateful for today?",
            "How did I help someone today?"
        };
    }

    public void WriteNewEntry()
    {
        Random rand = new Random();
        int promptIndex = rand.Next(prompts.Count);
        string prompt = prompts[promptIndex];

        Console.WriteLine($"\nPrompt: {prompt}");
        Console.Write("Your response: ");
        string response = Console.ReadLine();

        // New: Ask for additional info like emotion, weather, and location
        Console.Write("Emotion (optional): ");
        string emotionTag = Console.ReadLine();

        Console.Write("Weather (optional): ");
        string weather = Console.ReadLine();

        Console.Write("Location (optional): ");
        string location = Console.ReadLine();

        Entry entry = new Entry(prompt, response, emotionTag, weather, location);
        entries.Add(entry);

        Console.WriteLine("Entry added!\n");
    }

    // New Feature: Quick Notes Mode
    public void QuickNotesMode()
    {
        Console.Write("Quick note: ");
        string quickNote = Console.ReadLine();

        // Store the note with a generic prompt
        Entry entry = new Entry("Quick Note", quickNote);
        entries.Add(entry);

        Console.WriteLine("Quick note added!\n");
    }

    public void DisplayEntries()
    {
        if (entries.Count == 0)
        {
            Console.WriteLine("No entries to display.");
        }
        else
        {
            Console.WriteLine("\nJournal Entries:");
            foreach (Entry entry in entries)
            {
                Console.WriteLine(entry);
            }
        }
    }

    // Enhanced CSV Saving: Handles commas and quotes in content
    public void SaveToCSV(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Entry entry in entries)
            {
                string prompt = entry.Prompt.Replace("\"", "\"\"");
                string response = entry.Response.Replace("\"", "\"\"");
                writer.WriteLine($"\"{prompt}\",\"{response}\",\"{entry.Date}\",\"{entry.EmotionTag}\",\"{entry.Weather}\",\"{entry.Location}\"");
            }
        }
        Console.WriteLine("Journal saved successfully as CSV!");
    }

    // Save/Load from JSON format
    public void SaveToJson(string filename)
    {
        string json = JsonSerializer.Serialize(entries);
        File.WriteAllText(filename, json);
        Console.WriteLine("Journal saved successfully as JSON!");
    }

    public void LoadFromJson(string filename)
    {
        if (File.Exists(filename))
        {
            string json = File.ReadAllText(filename);
            entries = JsonSerializer.Deserialize<List<Entry>>(json);
            Console.WriteLine("Journal loaded successfully from JSON!");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}
