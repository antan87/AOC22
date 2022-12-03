using Common.Parsing;
using Common.Parsing.Interfaces;
using Day2;

Console.WriteLine("Day 2");
Console.WriteLine();
Console.WriteLine("Part 1");
Console.WriteLine();

string inputFile = @"Day2.Input.txt";
var input = await ParseHelper.GetInput(inputFile);

var text = input.Parse<string>(new string[] { Environment.NewLine },
                                 ParserCreator.StringParser);

var score = text.Select(row =>
                {
                    var textChunk = row.Parse<string>(new string[] { },
                                 ParserCreator.StringParser);

                    var opponentShape = textChunk[0].GetOpponentShape();
                    var playerShape = textChunk[1].GetPlayerShape();

                    var outcome = GameEvaluator.Play(opponentShape, playerShape);

                    return GamePointCalculator.CalculatePoints(playerShape, outcome);
                }).ToList().Sum();

Console.WriteLine(score);

Console.WriteLine();
Console.WriteLine("Part 2");
Console.WriteLine();

var scorePart2 = text.Select(row =>
{
    var textChunk = row.Parse<string>(new string[] { },
                 ParserCreator.StringParser);

    var outcome = GameEvaluator.TranslateGameOutcome(textChunk[1]);
    var opponentShape = textChunk[0].GetOpponentShape();
    var playerShape = outcome.GetPlayerShapeBasedOnOutcome(opponentShape);

    return GamePointCalculator.CalculatePoints(playerShape, outcome);
}).ToList().Sum();

Console.WriteLine(scorePart2);