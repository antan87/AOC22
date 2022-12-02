namespace Common.Parsing.Interfaces;

public interface IParser<T>
{
    T Parse(string value);
}