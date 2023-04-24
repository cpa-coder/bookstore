using System.Text.Json;
using BookStore.Models;
using BookStore.Options;

namespace BookStore.Commands;

public class AddCommand : CommandBase<Result<Book>, AddOptions>
{
    private readonly string _db;

    public AddCommand(string db)
    {
        _db = db;
    }

    public override Result<Book> Execute(string[] args)
    {
        base.Execute(args);

        var book = new Book
        {
            Id = Guid.NewGuid().ToString(),
            Author = Options!.Author,
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

        // add new item to the list
        books.Add(book);

        // save all the books
        var json = JsonSerializer.Serialize(books);
        File.WriteAllText(_db, json);

        return new Result<Book>(message: $"The book was successfully added with id {book.Id}");
    }
}