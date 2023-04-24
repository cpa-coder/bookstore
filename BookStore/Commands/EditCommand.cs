using System.Text.Json;
using BookStore.Models;
using BookStore.Options;
using CommandLine;

namespace BookStore.Commands;

public class EditCommand : CommandBase<Result<Book>, EditOptions>
{
    private readonly string _db;

    public EditCommand(string db)
    {
        _db = db;
    }

    public override Result<Book> Execute(string[] args)
    {
        base.Execute(args);
        
        var book = new Book
        {
            Id = Options!.Id,
            Author = Options.Author,
            Title = Options.Title,
        };

        // get list
        var books = new List<Book>();

        if (File.Exists(_db))
        {
            var data = File.ReadAllText(_db);
            var deserialize = JsonSerializer.Deserialize<List<Book>>(data);
            if (deserialize != null) books = deserialize;
        }

        //get the index of existing item with id
        var index = books.FindIndex(b => b.Id == Options.Id);

        //remove the item on index
        books.RemoveAt(index);

        //insert new book in the list at index
        books.Insert(index, book);

        // save all the books
        var json = JsonSerializer.Serialize(books);
        File.WriteAllText(_db, json);

        return new Result<Book>(message: $"The book was successfully edited with id {book.Id}");
    }
}