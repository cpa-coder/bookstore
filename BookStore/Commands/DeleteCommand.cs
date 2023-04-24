using System.ComponentModel;
using System.Text.Json;
using BookStore.Models;
using Ookii.CommandLine;
using Ookii.CommandLine.Commands;
using Spectre.Console;

namespace BookStore.Commands;

[Command("delete")]
[Description("Delete selected book to the database")]
public class DeleteCommand :ICommand
{

    [CommandLineArgument(argumentName:"id",IsRequired = true)]
    [Description("The id of the book.")]
    public string? Id { get; set; }
    
    public int Run()
    {
        // get list
        var books = new List<Book>();

        if (File.Exists(Database.Path))
        {
            var data = File.ReadAllText(Database.Path);
            var deserialize = JsonSerializer.Deserialize<List<Book>>(data);
            if (deserialize != null) books = deserialize;
        }

        //get the index of existing item with id
        var index = books.FindIndex(b => b.Id == Id);

        //remove the item on index
        books.RemoveAt(index);

        // save all the books
        var json = JsonSerializer.Serialize(books);
        File.WriteAllText(Database.Path, json);

        AnsiConsole.WriteLine($"The book with id {Id} was successfully deleted");
        return 0;
    }
}