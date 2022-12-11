using Common.Parsing;
using Common.Parsing.Interfaces;

namespace Day10;

internal class CommandParser : IParser<(ICommand command, int value)>
{
    public (ICommand command, int value) Parse(string value)
    {
        var parts = value.Split(" ");

        var command = GetCommand(parts[0]);
        if (command is AddxCommand)
            return (command, ParseHelper.Parse(ParserCreator.Int32Parser, parts[1]));

        return (command, 0);
    }

    private static ICommand GetCommand(string commandText)
    {
        switch (commandText)
        {
            case "addx":
                return new AddxCommand();

            case "noop":
                return new NoopCommand();

            default:
                throw new NotImplementedException();
        }
    }
}