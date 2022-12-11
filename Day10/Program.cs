using Common.Parsing;
using Day10;
using Microsoft.VisualBasic;

Console.WriteLine("Day 10");
Console.WriteLine();

string inputFile = @"Day10.Input.txt";
var input = await ParseHelper.GetInput(inputFile);
var values = input.Parse<string>(new string[] { Environment.NewLine },
                                 ParserCreator.StringParser, StringSplitOptions.RemoveEmptyEntries);

var commandAndValueTuples = values.Select(stringValue => ParseHelper.Parse(new CommandParser(), stringValue)).ToArray();

Part1(commandAndValueTuples);
Part2(commandAndValueTuples);

static void Part1((ICommand command, int value)[] commandAndValueTuples)
{
    Console.WriteLine();
    Console.WriteLine("Part 1");
    Console.WriteLine();

    int repValue = 1;
    int cycle = 0;
    List<CycleSnapshot> snapshots = new List<CycleSnapshot>();
    foreach (var tuple in commandAndValueTuples)
    {
        var resultTuple = tuple.command.Execute(cycle, repValue, tuple.value);
        repValue = resultTuple.Value;
        cycle = resultTuple.Snapshots.Max(x => x.Cycle);
        snapshots.AddRange(resultTuple.Snapshots);
    }

    List<int> signalStrenghts = new List<int>()
{
    20,
    60,
    100,
    140,
    180,
    220
};

    var signalResults = signalStrenghts.Select(signal => snapshots.First(snap => snap.Cycle == signal).Value * signal).ToArray();
    Console.WriteLine(signalResults.Sum()); ;
}

static void Part2((ICommand command, int value)[] commandAndValueTuples)
{
    Console.WriteLine();
    Console.WriteLine("Part 2");
    Console.WriteLine();

    var ctrRows = GetCtrRows(commandAndValueTuples).ToArray();

    foreach (var row in ctrRows)
    {
        Console.Write(string.Join("", row.Select(item => item).ToArray()));
        Console.WriteLine();
    }
}

static IEnumerable<IEnumerable<string>> GetCtrRows((ICommand command, int value)[] commandAndValueTuples)
{
    List<string> ctr = new List<string>();
    Spirit spirit = new Spirit(1);
    int cycle = -1;
    foreach (var tuple in commandAndValueTuples)
    {
        var resultTuple = tuple.command.Execute(cycle, spirit.Middle, tuple.value);
        cycle = resultTuple.Snapshots.Max(x => x.Cycle);

        foreach (var snapshot in resultTuple.Snapshots)
        {
            AddPrint(ctr, spirit, snapshot.Cycle);

            if (snapshot.Cycle == 39)
            {
                yield return ctr.ToArray();
                cycle = -1;
                ctr = new List<string>();

                if (resultTuple.Snapshots.Last().Cycle == 40)
                {
                    AddPrint(ctr, spirit, 0);
                    cycle = 0;
                    break;
                }
            }
        }
        spirit = spirit with { Middle = resultTuple.Value };
    }
}

static void AddPrint(List<string> ctr, Spirit spirit, int cycle)
{
    if (spirit.IsInRange(cycle))
        ctr.Add("#");
    else
        ctr.Add(".");
}