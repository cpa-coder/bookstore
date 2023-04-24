using BookStore.Commands;
using BookStore.Models;
using Spectre.Console;

if (args.Length == 0)
{
    Console.WriteLine("Please specify a command");
    return;
}

const string db = "library.json";

var trim = args[1..];

void Add()
{
    var cmd = new AddCommand(db);
    var result = cmd.Execute(trim);
    Console.WriteLine(result.Message);
}
void Edit()
{
    var cmd = new EditCommand(db);
    var result = cmd.Execute(trim);
    Console.WriteLine(result.Message);
}
void Delete()
{
    var cmd = new DeleteCommand(db);
    var result = cmd.Execute(trim);
    Console.WriteLine(result.Message);
}
void ShowList()
{
    var cmd = new ListCommand(db);
    var result = cmd.Execute(trim);
    ShowTable(result.Output);
}
void ShowTable(List<Book> books)
{
    var table = new Table();
    table.Border(TableBorder.Rounded);
    table.AddColumns(new TableColumn("id"), new TableColumn("title"), new TableColumn("author"));

    foreach (var book in books)
    {
        table.AddRow(book.Id, book.Title, book.Author);
    }

    AnsiConsole.Write(table);
}

var commands = new Dictionary<string, Action>
{
    { "list", ShowList },
    { "delete", Delete },
    { "edit", Edit },
    { "add", Add }
};

var arg = args[0];
commands.TryGetValue(arg, out var action);
action?.Invoke();