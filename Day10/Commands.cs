namespace Day10;

public interface ICommand
{
    (IEnumerable<CycleSnapshot> Snapshots, int Value) Execute(int cycle, int repositoryValuem, int value);
}

public record NoopCommand() : ICommand
{
    public (IEnumerable<CycleSnapshot> Snapshots, int Value) Execute(int cycle, int repositoryValuem, int value)
    {
        var snapshots = new List<CycleSnapshot> { new CycleSnapshot(cycle + 1, repositoryValuem) };
        return (snapshots, repositoryValuem);
    }
}

public record AddxCommand() : ICommand
{
    public (IEnumerable<CycleSnapshot> Snapshots, int Value) Execute(int cycle, int repositoryValue, int value)
    {
        var snapshots = new List<CycleSnapshot>
        {
            new CycleSnapshot(cycle + 1, repositoryValue),
            new CycleSnapshot(cycle + 2, repositoryValue)
        };

        return (snapshots, GetNewValue(repositoryValue, value));
    }

    private int GetNewValue(int repValue, int value)
    {
        return repValue + value;
    }
}