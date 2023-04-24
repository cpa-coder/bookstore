using System.ComponentModel;
using System.Text.Json;
using BookStore.Models;
using Ookii.CommandLine;
using Ookii.CommandLine.Commands;
using Spectre.Console;

namespace BookStore.Commands;

[Command("edit")]
[Description("Update selected book to the database")]
public class EditCommand : ICommand
{
    [CommandLineArgument("id", IsRequired = true)]
    [Description("The id of the book.")]
    public string? Id { get; set; }

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
            Id = Id!,
            Author = Author!,
            Title = Title!
        };

        // get list
        var books = Database.GetAll();

        //check if title already exists
        var titleExists = books.Any(b => b.Title.ToLower() == book.Title.ToLower() && b.Id != book.Id);
        if (titleExists)
        {
            AnsiConsole.MarkupLine($"[red]The book with title \"{book.Title}\" already exists[/]");
            return 1;
        }

        //get the index of existing item with id
        var index = books.FindIndex(b => b.Id == Id);

        //remove the item on index
        books.RemoveAt(index);

        //insert new book in the list at index
        books.Insert(index, book);

        // save all the books
        Database.Save(books);

        AnsiConsole.WriteLine($"The book with id {book.Id} was successfully updated");
        return 0;
    }
}