using Common.Parsing;
using Day11;

Console.WriteLine("Day 11");
Console.WriteLine();

string inputFile = @"Day11.Input.txt";
var input = await ParseHelper.GetInput(inputFile);
var values = input.Parse<string>(new string[] { Environment.NewLine + Environment.NewLine },
                                 ParserCreator.StringParser, StringSplitOptions.RemoveEmptyEntries);

var monkeys = values.Select(stringValue => ParseHelper.Parse(new MonkeyParser(), stringValue)).ToArray();

int rounds = 10000;
for (int counter = 0; counter < rounds; counter++)
{
    foreach (Monkey? monkey in monkeys)
    {
        var items = monkey.Throws(false).ToArray();
        foreach (var item in items)
        {
            var foundMonkey = monkeys.First(otherMonkey => otherMonkey.Index == item.index);
            foundMonkey.Items.Add(item.result);
        }
    }
}

var twoHighestValues = monkeys.OrderByDescending(monk => monk.Inspect).Take(2).Select(item => item.Inspect);
Console.WriteLine(twoHighestValues.Aggregate((a, b) => a * b));