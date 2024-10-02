using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        List<Scripture> scriptures = LoadScriptures("scriptures.txt");
        Random rand = new Random();
        Scripture scripture = scriptures[rand.Next(scriptures.Count)];

        RunProgram(scripture);
    }

    static void RunProgram(Scripture scripture)
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());

            if (scripture.IsCompletelyHidden())
            {
                Console.WriteLine("All words are hidden. You've completed the memorization!");
                isRunning = false;
            }
            else
            {
                Console.WriteLine("Press Enter to hide more words, or type 'quit' to exit.");
                string input = Console.ReadLine();

                if (input.ToLower() == "quit")
                {
                    isRunning = false;
                }
                else
                {
                    scripture.HideRandomWords(3);
                }
            }
        }
    }

    static List<Scripture> LoadScriptures(string filePath)
    {
        List<Scripture> scriptures = new List<Scripture>();
        string[] lines = File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            string[] parts = line.Split('|');
            if (parts.Length == 2)
            {
                scriptures.Add(new Scripture(parts[0], parts[1]));
            }
        }

        return scriptures;
    }
}

class Scripture
{
    private List<Word> words = new List<Word>();
    private Reference reference;

    public Scripture(string referenceText, string scriptureText)
    {
        this.reference = new Reference(referenceText);
        string[] wordArray = scriptureText.Split(' ');

        foreach (string word in wordArray)
        {
            words.Add(new Word(word));
        }
    }

    public void HideRandomWords(int numberToHide)
    {
        Random rand = new Random();
        for (int i = 0; i < numberToHide; i++)
        {
            int index = rand.Next(words.Count);
            words[index].Hide();
        }
    }

    public string GetDisplayText()
    {
        string displayText = reference.GetDisplayText() + "\n";
        foreach (Word word in words)
        {
            displayText += word.GetDisplayText() + " ";
        }
        return displayText.Trim();
    }

    public bool IsCompletelyHidden()
    {
        foreach (Word word in words)
        {
            if (!word.IsHidden())
            {
                return false;
            }
        }
        return true;
    }
}

class Word
{
    private string text;
    private bool isHidden;

    public Word(string text)
    {
        this.text = text;
        this.isHidden = false;
    }

    public void Hide()
    {
        isHidden = true;
    }

    public bool IsHidden()
    {
        return isHidden;
    }

    public string GetDisplayText()
    {
        if (isHidden)
        {
            return new string('_', text.Length);
        }
        return text;
    }
}

class Reference
{
    private string book;
    private int chapter;
    private int verseStart;
    private int? verseEnd;

    public Reference(string referenceText)
    {
        string[] parts = referenceText.Split(new char[] { ' ', ':', '-' });
        book = parts[0];
        chapter = int.Parse(parts[1]);
        verseStart = int.Parse(parts[2]);

        if (parts.Length > 3)
        {
            verseEnd = int.Parse(parts[3]);
        }
    }

    public string GetDisplayText()
    {
        if (verseEnd.HasValue)
        {
            return $"{book} {chapter}:{verseStart}-{verseEnd}";
        }
        return $"{book} {chapter}:{verseStart}";
    }
}

// Instead of just one scripture, the program now pulls from a file (scriptures.txt). So, you can have a bunch of different scriptures to choose from.
// When you start the program, it randomly picks one scripture from the file. This keeps things fresh every time you run it.
// There's a new method that reads the scriptures from the file. Each line in the file needs to be in the format: Reference|Text.
// The program clears the screen and shows the selected scripture and its reference, helping you see how you're doing with memorization as you hide words.
// It now asks you if you want to hide more words or quit, making it more interactive.