using System.Text.Json;
using BookStore.Models;
using BookStore.Options;
using CommandLine;

namespace BookStore.Commands;

public class DeleteCommand : CommandBase<Result<Book>, DeleteOptions>
{
    private readonly string _db;

    public DeleteCommand(string db)
    {
        _db = db;
    }

    public override Result<Book> Execute(string[] args)
    {
        base.Execute(args);

        // get list
        var books = new List<Book>();

        if (File.Exists(_db))
        {
            var data = File.ReadAllText(_db);
            var deserialize = JsonSerializer.Deserialize<List<Book>>(data);
            if (deserialize != null) books = deserialize;
        }

        //get the index of existing item with id
        var index = books.FindIndex(b => b.Id == Options!.Id);

        //remove the item on index
        books.RemoveAt(index);

        // save all the books
        var json = JsonSerializer.Serialize(books);
        File.WriteAllText(_db, json);

        return new Result<Book>(message: $"The book was successfully deleted with id {Options!.Id}");
    }
}