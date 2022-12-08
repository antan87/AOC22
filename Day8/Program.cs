using Common.Parsing;
using Day8;

Console.WriteLine("Day 8");
Console.WriteLine();
Console.WriteLine("Part 1");
Console.WriteLine();

string inputFile = @"Day8.Input.txt";

var input = await ParseHelper.GetInput(inputFile);

var textRows = input.Parse<string>(new string[] { Environment.NewLine },
                                 ParserCreator.StringParser);

var array = new int[textRows.Length, textRows[0].Count()];
for (int y = 0; y < textRows.Length; y++)
{
    var line = textRows[y];
    var rows = line.Select(charItem => charItem.ToString()).Select(number => ParseHelper.Parse(ParserCreator.Int32Parser, number)).ToArray();
    for (int x = 0; x < rows.Count(); x++)
        array[y, x] = rows[x];
}

List<Tree> trees = new List<Tree>();
for (int y = 0; y < array.GetLength(0); y++)
{
    for (int x = 0; x < array.GetLength(1); x++)
    {
        var height = array[y, x];

        var tree = new Tree(height, new Point(x, y), array.GetLength(1) - 1, array.GetLength(0) - 1);
        trees.Add(tree);
    }
}

var visableTrees = trees.Where(x => x.IsVisible(trees)).ToArray();

Console.WriteLine(visableTrees.Count());

Console.WriteLine();
Console.WriteLine("Part 2");
Console.WriteLine();

var highestScenic = trees.Max(x => x.GetScenicScore(trees));

Console.WriteLine(highestScenic);