using CommandLine;

namespace BookStore.Options;

public class DeleteOptions
{
    [Option('i', "id", Required = true, HelpText = "Id is required")]
    public string Id { get; set; }
}