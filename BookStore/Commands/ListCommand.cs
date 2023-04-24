using System.ComponentModel;
using BookStore.Models;
using Ookii.CommandLine;
using Ookii.CommandLine.Commands;
using Spectre.Console;

namespace BookStore.Commands;

[Command("list")]
[Description("Show the list of books from the database")]
public class ListCommand : ICommand
{
    [CommandLineArgument("sort", ShortName = 's', IsRequired = false, DefaultValue = "desc")]
    [Description("How the list should be sorted.")]
    public string? Sort { get; set; }

    public int Run()
    {
        if (Sort != "asc" && Sort != "desc")
        {
            AnsiConsole.MarkupLine("[red]Invalid sort option. Use [bold]asc[/] or [bold]desc[/][/]");
            return 1;
        }

        // get list
        var books = Database.GetAll();

        if (books.Count == 0)
        {
            AnsiConsole.MarkupLine("[green]No books found in the database.[/]");
            return 0;
        }

        switch (Sort)
        {
            case "asc":
                ShowTable(books.OrderBy(x => x.Title).ToList());
                break;
            case "desc":
                ShowTable(books.OrderByDescending(x => x.Title).ToList());
                break;
        }

        return 0;
    }

    private void ShowTable(List<Book> books)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumns(new TableColumn("id"), new TableColumn("title"), new TableColumn("author"));

        foreach (var book in books) table.AddRow(book.Id, book.Title, book.Author);

        AnsiConsole.Write(table);
    }
}