using System.Text.Json;

namespace BookStore.Models;

public static class Database
{
    public static string Path { get; set; } = "db.json";

    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    public static void Save(List<Book> books)
    {
        var json = JsonSerializer.Serialize(books, Options);
        File.WriteAllText(Path, json);
    }

    public static List<Book> GetAll()
    {
        var books = new List<Book>();

        if (!File.Exists(Path)) return books;

        var data = File.ReadAllText(Path);
        var deserialize = JsonSerializer.Deserialize<List<Book>>(data, Options);
        if (deserialize != null) books = deserialize;

        return books;
    }
}