using System.Collections.Immutable;
using Common.Parsing;
using Day3;

Console.WriteLine("Day 3");
Console.WriteLine();
Console.WriteLine("Part 1");
Console.WriteLine();

string inputFile = @"Day3.Input.txt";
var input = await ParseHelper.GetInput(inputFile);

var textRows = input.Parse<string>(new string[] { Environment.NewLine },
                                 ParserCreator.StringParser);

var rucksacks = textRows.Select(textRow =>
{
    var items = textRow.Select(letter => letter.ToString());

    var take = items.Count() / 2;

    var compartmentOne = new Compartment(items.Take(take));
    var compartmentTwo = new Compartment(items.Skip(take).Take(take));

    return new Rucksack(new List<Compartment> { compartmentOne, compartmentTwo });
}).ToArray();

var duplicates = rucksacks.Select(rucksack => rucksack.FindDuplicate()).ToArray();

List<string> letters = new List<string> {
    "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m",
    "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
    "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
    "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
};

var sum = duplicates.Select(duplicate => letters.IndexOf(duplicate) + 1).ToList().Sum();

Console.WriteLine(sum);