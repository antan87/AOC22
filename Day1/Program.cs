using System.Linq;
using Common.Parsing;
using Common.Parsing.Interfaces;
using Day1;

Console.WriteLine("Day 1");
Console.WriteLine();

string inputFile = @"Day1.Input.txt";
var input = await ParseHelper.GetInput(inputFile);
var values = input.Parse<string>(new string[] { Environment.NewLine + Environment.NewLine },
                                 ParserCreator.StringParser,
                                 StringSplitOptions.TrimEntries);

var elfs = values.Select(textChunk => new Elf(textChunk.Parse<int>(new[] { Environment.NewLine }, ParserCreator.Int32Parser))).ToList();

Console.WriteLine("Part 1:");
Console.WriteLine(elfs.Max(elf => elf.Sum()));
Console.WriteLine();
Console.WriteLine("Part 2:");
Console.WriteLine(elfs.OrderByDescending(elf => elf.Sum()).Take(3).Sum(elf => elf.Sum()));