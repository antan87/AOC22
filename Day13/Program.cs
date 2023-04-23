// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Immutable;
using Common.Parsing;
using Day13;

Console.WriteLine("Day 13");

string inputFile = @"Day13.Input.txt";
var input = await ParseHelper.GetInput(inputFile);

var rows = ParseHelper.Parse(new RowParser(), input);
var indexes = new List<int>();
Part1(indexes, rows, 0);

Console.WriteLine("Part one");
Console.WriteLine(indexes.Sum());

Console.WriteLine();

Console.WriteLine("Part two");
Console.WriteLine(Part2(rows));

void Part1(List<int> indexes, List<List<IPart>> partsList, int skipper)
{
    if (skipper >= partsList.Count())
        return;

    var parts = partsList.Skip(skipper).Take(2);
    if (Comparer(parts.First(), parts.Last()) == Result.Left)
        indexes.Add((skipper + 2) / 2);

    skipper += 2;
    Part1(indexes, partsList, skipper);
}

int Part2(List<List<IPart>> partsList)
{
    List<IPart> decoder = new List<IPart>();
    decoder.Add(new StartPart());
    decoder.Add(new IntPart(2, true));
    decoder.Add(new EndPart());

    List<IPart> decoder2 = new List<IPart>();
    decoder2.Add(new StartPart());
    decoder2.Add(new IntPart(6, true));
    decoder2.Add(new EndPart());

    partsList.Add(decoder);
    partsList.Add(decoder2);

    List<List<IPart>> orderdList = new List<List<IPart>>();
    foreach (var partList in partsList)
    {
        if (!orderdList.Any())
        {
            orderdList.Add(partList);
            continue;
        }
        int index = 0;
        foreach (var orderdPartList in orderdList)
        {
            var result = Comparer(partList, orderdPartList);
            if (result == Result.Equal)
                throw new Exception("This should not be able to happend.");

            if (result == Result.Left)
                break;

            index++;
        }
        orderdList.Insert(index, partList);
    }

    List<int> indexes = new List<int>();
    for (int index = 0; index < orderdList.Count(); index++)
    {
        if (orderdList[index].Any(part => part is IntPart && ((IntPart)part).Marker))
            indexes.Add(index + 1);
    }

    return indexes.Aggregate(1, (a, b) => a * b);
}

Result Comparer(IEnumerable<IPart> partsOne, IEnumerable<IPart> partsTwo)
{
    var mover = new Mover(partsOne.ToImmutableList());
    var mover2 = new Mover(partsTwo.ToImmutableList());

    return GetResult(mover, mover2);

    Result GetResult(Mover moverOne, Mover moverTwo)
    {
        var part = moverOne.GetPart();
        var partTwo = moverTwo.GetPart();
        switch (part, partTwo)
        {
            case (null, null):
                return Result.Equal;

            case (StartPart _, StartPart _):
            case (EndPart _, EndPart _):
            case (IntPart intPart, IntPart intPartTwo) when intPart.Value == intPartTwo.Value:
                {
                    moverOne.Move();
                    moverTwo.Move();

                    return GetResult(moverOne, moverTwo);
                }

            case (IntPart intPart, StartPart _):
                moverOne = moverOne.Insert(intPart.Value);

                return GetResult(moverOne, moverTwo);

            case (StartPart _, IntPart intPart):
                moverTwo = moverTwo.Insert(intPart.Value);

                return GetResult(moverOne, moverTwo);

            case (EndPart, _):
            case (null, _):
            case (IntPart intPart, IntPart intPartTwo) when intPart.Value < intPartTwo.Value:
                return Result.Left;

            case (_, EndPart):
            case (_, null):
            case (IntPart intPart, IntPart intPartTwo) when intPart.Value > intPartTwo.Value:
                return Result.Right;

            default: throw new ArgumentException();
        }
    }
}

record Mover(ImmutableList<IPart> Parts)
{
    private int _index;
    public void Move()
    {
        ++_index;
    }
    public IPart? GetPart()
    {
        return Parts.Count() > _index ? Parts[_index] : null;
    }

    public Mover Insert(int number)
    {
        var newList = this.Parts.ToList();
        newList[_index] = new StartPart();
        newList.Insert(_index + 1, new IntPart(number));
        newList.Insert(_index + 2, new EndPart());

        return this with { Parts = newList.ToImmutableList() };
    }
}

public record struct IntPart(int Value, bool Marker = false) : IPart;
public record struct EndPart() : IPart;
public record struct StartPart() : IPart;

public interface IPart
{ }

public enum Result
{
    Equal,
    Left,
    Right
}