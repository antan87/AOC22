using Common.Parsing;
using Day9;

Console.WriteLine("Day 9");
Console.WriteLine();
Console.WriteLine("Part 1");
Console.WriteLine();

string inputFile = @"Day9.Input.txt";
var input = await ParseHelper.GetInput(inputFile);
var values = input.Parse<string>(new string[] { Environment.NewLine },
                                 ParserCreator.StringParser, StringSplitOptions.RemoveEmptyEntries);

var steps = values.Select(stringValue => ParseHelper.Parse(new StepParser(), stringValue)).ToArray();

Part1(steps);
Part2(steps);

static void Part1(Step[] steps)
{
    Console.WriteLine();
    Console.WriteLine("Part 1");
    Console.WriteLine();
    var head = new Head(new Point());
    var tail = new Tail();
    foreach (var step in steps)
    {
        for (int i = 0; i < step.Steps; i++)
        {
            head = head.Move(step.MovmentKind, 1);
            tail = tail.FollowHead(head);
        }
    }

    Console.WriteLine(tail.Steps.Count());
}

static void Part2(Step[] steps)
{
    Console.WriteLine();
    Console.WriteLine("Part 2");
    Console.WriteLine();
    var head = new Head(new Point());
    var tail = new Tail();
    var tail2 = new Tail();
    var tail3 = new Tail();
    var tail4 = new Tail();
    var tail5 = new Tail();
    var tail6 = new Tail();
    var tail7 = new Tail();
    var tail8 = new Tail();
    var tail9 = new Tail();
    foreach (var step in steps)
    {
        for (int i = 0; i < step.Steps; i++)
        {
            head = head.Move(step.MovmentKind, 1);
            tail = tail.FollowHead(head);
            tail2 = tail2.FollowHead(tail);
            tail3 = tail3.FollowHead(tail2);
            tail4 = tail4.FollowHead(tail3);
            tail5 = tail5.FollowHead(tail4);
            tail6 = tail6.FollowHead(tail5);
            tail7 = tail7.FollowHead(tail6);
            tail8 = tail8.FollowHead(tail7);
            tail9 = tail9.FollowHead(tail8);
        }
    }

    Console.WriteLine(tail9.Steps.Count());
}