using CommandLine;

namespace BookStore.Options;

public class ListOptions
{
    [Option('s', "sort", Required = false, HelpText = "Sort by title or author")]
    public Sort Sort { get; set; }
}

public enum Sort
{
    Ascending = 0,
    Descending = 1
}