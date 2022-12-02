namespace Day1;
public record Elf(IEnumerable<int> Numbers)
{
    public int Sum() => Numbers.Sum();
}