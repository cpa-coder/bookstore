using System.Text.Json;
using BookStore.Models;
using BookStore.Options;

namespace BookStore.Commands;

public class ListCommand : CommandBase<Result<List<Book>>, ListOptions>
{
    private readonly string _db;

    public ListCommand(string db)
    {
        _db = db;
    }

    public override Result<List<Book>> Execute(string[] args)
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

        if (Options!.Sort == Sort.Ascending)
            return new Result<List<Book>> { Output = books.OrderBy(x => x.Title).ToList() };

        return new Result<List<Book>> { Output = books.OrderByDescending(x => x.Title).ToList() };
    }
}