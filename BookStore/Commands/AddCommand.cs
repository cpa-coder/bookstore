using System.ComponentModel;
using System.Text.Json;
using BookStore.Models;
using Ookii.CommandLine;
using Ookii.CommandLine.Commands;
using Spectre.Console;

namespace BookStore.Commands;

[Command("add")]
[Description("Add a new book to the database")]
public class AddCommand : ICommand
{
    [CommandLineArgument("author", ShortName = 'a', IsRequired = true)]
    [Description("The author of the book.")]
    public string? Author { get; set; }

    [CommandLineArgument("title", ShortName = 't', IsRequired = true)]
    [Description("The title of the book.")]
    public string? Title { get; set; }

    public int Run()
    {
        var book = new Book
        {
            Id = Guid.NewGuid().ToString(),
            Author = Author!,
            Title = Title!
        };

        // get list
        var books = Database.GetAll();

        //check if title already exists
        var titleExists = books.Any(b => b.Title.ToLower() == book.Title!.ToLower());
        if (titleExists)
        {
            AnsiConsole.MarkupLine($"[red]The book with title \"{book.Title}\" already exists[/]");
            return 1;
        }

        // add new item to the list
        books.Add(book);

        // save all the books
        Database.Save(books);

        AnsiConsole.WriteLine($"The book was successfully added with id {book.Id}");
        return 0;
    }
}