using Common.Parsing.Interfaces;

namespace Common.Parsing;

public class Int32Parser : IParser<int>
{
    public int Parse(string value) => int.Parse(value);
}