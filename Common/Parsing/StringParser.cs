using Common.Parsing.Interfaces;

namespace Common.Parsing;

public class StringParser : IParser<string>
{
    public string Parse(string value) => value;
}