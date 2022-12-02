namespace Common.Parsing.Interfaces;

public interface IParser2D<T>
{
    T[] Parse(int y, string value);
}