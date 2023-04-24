using Ookii.CommandLine;
using Ookii.CommandLine.Commands;

var options = new CommandOptions
{
    Mode = ParsingMode.LongShort,
    ArgumentNameComparer = StringComparer.InvariantCulture,
    ArgumentNameTransform = NameTransform.DashCase,
    ValueDescriptionTransform = NameTransform.DashCase
};

var manager = new CommandManager(options);
return manager.RunCommand() ?? 1;