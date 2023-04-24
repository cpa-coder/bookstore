using CommandLine;

namespace BookStore.Options;

public class EditOptions
{
    [Option('i', "id", Required = true, HelpText = "Id is required")]
    public string Id { get; set; }

    [Option('t', "title", Required = true, HelpText = "Title is required")]
    public string Title { get; set; }

    [Option('a', "author", Required = true, HelpText = "Author is required")]
    public string Author { get; set; }
}