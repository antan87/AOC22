using Common.Parsing.Interfaces;

namespace Common.Parsing;

public static class ParserCreator
{
    public static Int32Parser Int32Parser = new Int32Parser();
    public static StringParser StringParser = new StringParser();
    public static Int32Parser2D Int32Parser2D = new Int32Parser2D();
}