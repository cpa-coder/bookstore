using CommandLine;

namespace BookStore.Commands;

public class CommandBase<T, TOptions> : ICommand<T>
{
   protected IEnumerable<Error>? Errors;
   protected TOptions? Options;

    public virtual T? Execute(string[] args)
    {
        Parser.Default.ParseArguments<TOptions>(args)
            .WithParsed(RunOptions)
            .WithNotParsed(HandleParseError);

        return default;
    }

    private void HandleParseError(IEnumerable<Error> errors)
    {
        Errors = errors;
    }

    private void RunOptions(TOptions opt)
    {
        Options = opt;
    }
}