using CommandLine;

namespace BookStore.Options;

public class AddOptions
{
    [Option('t', "title", Required = true, HelpText = "Title is required")]
    public string Title { get; set; }

    [Option('a', "author", Required = true, HelpText = "Author is required")]
    public string Author { get; set; }
}