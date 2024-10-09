using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Video video1 = new Video("Learn C# Basics", "Adrian De Leon", 600);
        Video video2 = new Video("C# for Beginners", "Adri Trillanes", 900);

        video1.AddComment("Alice", "This video is very helpful!");
        video1.AddComment("Bob", "Thanks for the explanation.");

        video2.AddComment("Eve", "Great tutorial, I learned a lot!");
        video2.AddComment("Charlie", "Please make more videos like this.");

        List<Video> videos = new List<Video> { video1, video2 };

        foreach (var video in videos)
        {
            Console.WriteLine("Title: " + video.Title);
            Console.WriteLine("Author: " + video.Author);
            Console.WriteLine("Length: " + video.Length + " seconds");
            Console.WriteLine("Number of comments: " + video.GetNumberOfComments());

            foreach (var comment in video.Comments)
            {
                Console.WriteLine(comment.Name + ": " + comment.Text);
            }

            Console.WriteLine();
        }
    }
}

class Video
{
    public string Title;
    public string Author;
    public int Length;
    public List<Comment> Comments;

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = new List<Comment>();
    }

    public void AddComment(string name, string text)
    {
        Comment newComment = new Comment(name, text);
        Comments.Add(newComment);
    }

    public int GetNumberOfComments()
    {
        return Comments.Count;
    }
}

class Comment
{
    public string Name;
    public string Text;

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}
