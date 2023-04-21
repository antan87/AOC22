// See https://aka.ms/new-console-template for more information
using System;
using Common.Parsing;
using Day13;

Console.WriteLine("Day 13");

string inputFile = @"Day13.Input.txt";
var input = await ParseHelper.GetInput(inputFile);

var pairs = ParseHelper.Parse(new PairParser(), input);

var rightOrders = pairs.Where(pair => pair.RightOrder()).ToList();
Console.WriteLine("Part one");
Console.WriteLine(rightOrders.Sum(pair => pair.Index));

public record Pair(int Index, ListPart PartOne, ListPart PartTwo)
{
    public bool RightOrder()
    {
        var result = Check(PartOne.Move(), PartTwo.Move());
        if (result == Result.Equal)
            throw new Exception();

        return result == Result.Left;

        static Result Check(ListPart? listPartOne, ListPart? listPartTwo)
        {
            Result result = Result.Equal;
            while (result == Result.Equal)
            {
                switch (listPartOne, listPartTwo)
                {
                    case (null, null):

                        return Result.Equal;

                    case (null, _):
                        {
                            return Result.Left;
                        }

                    case (_, null):
                        {
                            return Result.Right;
                        }
                }

                var partOne = listPartOne.GetPart();
                var partTwo = listPartTwo.GetPart();

                switch (partOne, partTwo)
                {
                    case (IntPart intPart, IntPart intPartTwo):
                        {
                            if (intPart.Value == intPartTwo.Value)
                            {
                                result = Result.Equal;
                                listPartOne = listPartOne.Move();
                                listPartTwo = listPartTwo.Move();
                            }
                            else if (intPart.Value < intPartTwo.Value)
                                result = Result.Left;
                            else
                                result = Result.Right;

                            break;
                        }

                    case (ListPart nextListPart, ListPart nextlistPartTwo):
                        {
                            result = Check(nextListPart.Move(), nextlistPartTwo.Move());
                            break;
                        }

                    case (IntPart intPart, ListPart nextlistPartTwo):
                        {
                            var newListPart = new ListPart(new List<IPart> { intPart }, listPartOne);
                            listPartOne.Insert(newListPart);
                            result = Check(newListPart.Move(), nextlistPartTwo.Move());
                            break;
                        }

                    case (ListPart nextListPart, IntPart intPartTwo):
                        {
                            var newListPart = new ListPart(new List<IPart> { intPartTwo }, listPartTwo);
                            listPartTwo.Insert(newListPart);
                            return Check(nextListPart.Move(), newListPart.Move());
                        }

                    default:
                        throw new Exception("Missing mapping");
                }
            }

            return result;
        }
    }
}

public record ListPart(List<IPart> Parts, ListPart? ParentListPart) : IPart
{
    private int Index { get; set; } = -1;

    public ListPart? Move()
    {
        this.Index++;
        if (this.Index == this.Parts.Count())
            return ParentListPart?.Move() ?? null;

        return this;
    }

    public void Insert(ListPart part)
    {
        this.Parts[Index] = part;
    }

    public IPart GetPart() => this.Parts[Index];
}

public record IntPart(int Value) : IPart;

public interface IPart
{
}

public enum Result
{
    Equal,
    Left,
    Right
}